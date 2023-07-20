namespace ModbusTCPIP
{
    public class ModBusFrameCreator
    {
        private static int transactionId = 0;

        //Create complete Modbus Frame ready to send via TCP/IP
        public static byte[] CreateFrame(int unitId, int functionId, params int[] dataForFunction)
        {
            byte[] frame;
            byte[] dataPDU;
            byte[] dataMBAP = new byte[7];


            //Selection to create correct data frame to send based on function code
            switch (functionId)
            {
                case 1:
                    Console.WriteLine("[Frame: function code 1 -> Read Coils]");
                    dataPDU = ReadingPDU(dataForFunction[0], dataForFunction[1], 1);
                    break;
                case 2:
                    Console.WriteLine("[Frame: function code 2 -> Read Discrete Inputs]");
                    dataPDU = ReadingPDU(dataForFunction[0], dataForFunction[1], 2);
                    break;
                case 3:
                    Console.WriteLine("[Frame: function code 3 -> Read Holding Registers]");
                    dataPDU = ReadingPDU(dataForFunction[0], dataForFunction[1], 3);
                    break;
                case 4:
                    Console.WriteLine("[Frame: function code 4 -> Read Input Registers]");
                    dataPDU = ReadingPDU(dataForFunction[0], dataForFunction[1], 4);
                    break;
                case 5:
                    Console.WriteLine("[Frame: function code 5 -> Write Single Coil]");
                    dataPDU = SingleWritingPDU(dataForFunction[0], dataForFunction[1], 5);
                    break;
                case 6:
                    Console.WriteLine("[Frame: function code 6 -> Write Single Holding Register]");
                    dataPDU = SingleWritingPDU(dataForFunction[0], dataForFunction[1], 6);
                    break;
                case 16:
                    Console.WriteLine("[Frame: function code 16 -> Write Holding Registers]");
                    int[] temp = new int[dataForFunction[1]];
                    Array.Copy(dataForFunction, 2, temp, 0, dataForFunction[1]);
                    dataPDU = MultipleWritingPDU(dataForFunction[0], dataForFunction[1], 16, temp);
                    break;
                default:
                    throw new FrameException("Function " + functionId + " is not supported");
            }
            dataMBAP = CreateMBAPHeader(unitId, dataPDU.Length);
            frame = dataMBAP.Concat(dataPDU).ToArray();
            return frame;
        }

        //Decode Modbus Frame from given data
        //We start iterate over values from 9 because 0,1 is Transaction Id;
        //                                            2,3 is Protocol Id;
        //                                            4,5 is bytes length;
        //                                            6 is unit id;
        //                                            7 is function code;
        //                                            8 is length of registers;
        public static List<int> DecodeModbusFrame(byte[] responseModbusFrame)
        {
            if (responseModbusFrame.Length < 7) throw new FrameException("Incorrect Modbus frame. Decode is not possible.");
            List<int> values = new List<int>();
            increaseTransactionId();
            int DataLength = CombineBytes(responseModbusFrame[4], responseModbusFrame[5]);
            int doneFunction = responseModbusFrame[7];
            Console.WriteLine();
            for (int i = 9; i < responseModbusFrame.Length - 1; i += 2)
            {
                values.Add(CombineBytes(responseModbusFrame[i], responseModbusFrame[i + 1]));
            }
            return values;
        }

        //Create Protocol Data Unit For Reading Modbus Frames 
        public static byte[] ReadingPDU(int startAddress, int range, int functionCode)
        {
            if (functionCode == 1 || functionCode == 2)
            {
                if (range is > 2000 or < 1) throw new FrameException("Range must be between 1 and 2000.");
            } else if (functionCode == 4)
            {
                if (range is > 125 or < 1) throw new FrameException("Range must be between 1 and 125 for inputs.");
            }
            if (startAddress + range is > 65535) throw new FrameException("Out of range.");

            byte[] data = new byte[5];
            byte[] temp;

            //Convert function code to bytes
            byte byteFunctionCode = Convert.ToByte(functionCode);
            data[0] = byteFunctionCode;

            //Convert first address to byte
            temp = MakeModbusBytesConvention(startAddress);
            data[1] = temp[0];
            data[2] = temp[1];

            //Convert range to bytes
            temp = MakeModbusBytesConvention(range);
            data[3] = temp[0];
            data[4] = temp[1];

            return data;
        }

       
        //Create Protocol Data Unit for write single holding register
        public static byte[] SingleWritingPDU(int address, int value, int functionCode)
        {
            if (functionCode == 5 && (value != 65280 && value != 0)) throw new FrameException("Only turn on or off is permited on coil.");
            byte[] data = new byte[5];
            byte[] temp;

            //Convert Function code to bytes
            byte byteFunctionCode = Convert.ToByte(functionCode);
            data[0] = byteFunctionCode;

            //Convert adress
            temp = MakeModbusBytesConvention(address);
            data[1] = temp[0];
            data[2] = temp[1];

            //Convert value
            temp = MakeModbusBytesConvention(value);
            data[3] = temp[0];
            data[4] = temp[1];

            return data;
        }

        //Create Protocol Data Unit for write multiple holding register
        public static byte[] MultipleWritingPDU(int startAddress, int range, int functionCode, int[] values)
        {
            int amountBytesForCoils;
            byte[] data;
            byte[] temp;

            if (range != values.Length) throw new FrameException("Range must be the same as lenght of values array.");
            if (functionCode == 15)
            {
                if (range > 1968) throw new FrameException("Range of write coils must be max 1968");
                //Making correct amount of bytes for coils
                if(range % 8 == 0)
                {
                    amountBytesForCoils = (int)(range / 8);
                } else
                {
                    amountBytesForCoils = (int)(range / 8) + 1;
                }
                data = new byte[amountBytesForCoils + 6];
                data[5] = Convert.ToByte(amountBytesForCoils);
            } 
            else
            {
                if (range > 123) throw new FrameException("Range of write registers must be max 123");
                data = new byte[2 * range + 6];
                //Correct amount of bytes for registers values 
                data[5] = Convert.ToByte(range * 2);
            }

            //Convert Function code to bytes
            byte byteFunctionCode = Convert.ToByte(functionCode);
            data[0] = byteFunctionCode;

            //Convert start adress 
            temp = MakeModbusBytesConvention(startAddress);
            data[1] = temp[0];
            data[2] = temp[1];

            //Convert range
            temp = MakeModbusBytesConvention(range);
            data[3] = temp[0];
            data[4] = temp[1];

            //Convert register values to write
            for (int i = 6, j = 0; i < 2 * range + 5; i += 2, j++)
            {
                temp = MakeModbusBytesConvention(values[j]);
                data[i] = temp[0];
                data[i + 1] = temp[1];
            }

            return data;
        }

        //Create Modbus Application Protocol Header
        public static byte[] CreateMBAPHeader(int unitId, int dataLength)
        {
            if (unitId is <= 0 or > 255) throw new FrameException("Unit mus be between 0 and 255");
            if (dataLength == 65535) throw new FrameException("To large data. Data needed one byte less.");

            byte[] data = new byte[7];
            byte[] temp;

            //Convert Trnasaction identifier
            temp = MakeModbusBytesConvention(transactionId);
            data[0] = temp[0];
            data[1] = temp[1];

            //Protocol id bytes is deafult 00 00
            data[2] = 0x00;
            data[3] = 0x00;

            //Length of data and unit id in bytes
            dataLength += 1;
            temp = MakeModbusBytesConvention(dataLength);
            data[4] = temp[0];
            data[5] = temp[1];

            //Convert unit id for byte representation
            byte byteUnitID = Convert.ToByte(unitId);
            data[6] = byteUnitID;

            return data;
        }

        //Create correct bytes for transport in modbus protocol 
        private static byte[] MakeModbusBytesConvention(int value)
        {
            byte[] result = new byte[2];
            if (value is >= 0 and <= 255)
            {
                byte byteValue = Convert.ToByte(value);
                result[0] = 0x00;
                result[1] = byteValue;
            }
            else if (value is >= 256 and <= 65535)
            {
                byte[] bytesValue = BitConverter.GetBytes(value);
                result[0] = bytesValue[1];
                result[1] = bytesValue[0];
            }
            else
            {
                throw new FrameException("Incorrect value, out of range, it should be in 0...65535. Modbus frame support max 2 bytes range of unsigned int.");
            }
            return result;
        }

        //Convert bytes in correct way to restore values
        private static int CombineBytes(byte firstByte, byte secondByte)
        {
            if (firstByte == 0x00)
            {
                return secondByte;
            }
            return firstByte << 8 | secondByte;
        }

        //Make correct Transaction id for slave 
        private static void increaseTransactionId()
        {
            if (transactionId > 65535)
            {
                transactionId = 0;
            }
            else
            {
                transactionId++;
            }

        }
    }

}