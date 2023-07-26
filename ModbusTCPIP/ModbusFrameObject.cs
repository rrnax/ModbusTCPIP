namespace ModbusTCPIP
{
    public class ModbusFrameObject
    {
        public byte[] orginalFrame { get; }
        public int transactionId { get; }
        public int protocolId { get; }
        public int amountBytesOfPDU { get; }
        public int unitId { get; }  
        public int functionCode { get; }
        public int dataBytesAmount {  get; set; }
        public string dataType { get; set; }
        public List<int> registers { get; set; }
        public List<bool> oneStateValuesList { get; set; }

        //Initialize and read Protocol Header
        //Decode Modbus Frame from given data
        //0,1 is Transaction Id;
        //2,3 is Protocol Id;
        //4,5 is bytes length;
        //6 is unit id;
        //7 is function code;
        public ModbusFrameObject(byte[] frame)
        {
            if (frame.Length < 7) throw new FrameException("Incorrect Modbus frame. Decode is not possible.");
            this.orginalFrame = frame;
            this.transactionId = this.CombineBytes(frame[0], frame[1]);
            this.protocolId = this.CombineBytes(frame[2], frame[3]);
            this.amountBytesOfPDU = this.CombineBytes(frame[4], frame[5]);  //Protocol Data Unit Length
            this.unitId = Convert.ToInt32(frame[6]);
            this.functionCode = Convert.ToInt32(frame[7]);
        }

        //Decode Coils, Discrete inputs
        public List<bool> DecodeBits()
        {
            this.dataBytesAmount = Convert.ToInt32(this.orginalFrame[8]);
            this.dataType = "state";
            List<bool> result = new List<bool>();
            if (this.functionCode == 1 || this.functionCode == 2)
            {
                string reversCoils = "";
                for(int i = 9; i < this.orginalFrame.Length; i++)
                {
                    string oneRange = (Convert.ToString(this.orginalFrame[i], 2).PadLeft(8, '0'));
                    reversCoils += new string(oneRange.Reverse().ToArray());
                }
                for(int i = reversCoils.Length - 1; i >= 0; i--)
                {
                    if (reversCoils[i] == '0') reversCoils = reversCoils.Remove(i);
                    else break;
                }
                foreach (char bit in reversCoils)
                {
                    if (bit == '0') result.Add(false);
                    else result.Add(true);
                }
                this.oneStateValuesList = result;
                return result;
            }
            else
            {
                throw new FrameException("Inncorect method to decode given frame");
            }
        }

        //Decode input register and holding registers
        public List<int> DecodeRegisters()
        {
            this.dataBytesAmount = Convert.ToInt32(this.orginalFrame[8]);
            this.dataType = "register";
            List<int> values = new List<int>();
            if(this.functionCode == 3 || this.functionCode == 4) 
            {
                for (int i = 9; i < this.orginalFrame.Length - 1; i += 2)
                {
                    values.Add(this.CombineBytes(this.orginalFrame[i], this.orginalFrame[i + 1]));
                }
                this.registers = values;
                return values;
            }
            else
            {
                throw new FrameException("Inncorect method to decode given frame");
            }
        }

        //Convert bytes in correct way to restore values
        private int CombineBytes(byte firstByte, byte secondByte)
        {
            if (firstByte == 0x00)
            {
                return secondByte;
            }
            return firstByte << 8 | secondByte;
        }
    }
}
