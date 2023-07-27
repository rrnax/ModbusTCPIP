using System.Net;
using System.Net.Sockets;

namespace ModbusTCPIP
{
    public class ModbusConnection
    {
        private string slaveIp { get; }
        private int slavePort { get; }
        private Socket slaveSocket { get; set; }
        private int slaveUnitId { get; set; }   
        private bool connected { get; set; }    


        //Constructors
        public ModbusConnection()
        {
            this.slaveIp = "127.0.0.1";
            this.slavePort = 502;
            this.slaveUnitId = 1;
            this.connected = false;
        }

        public ModbusConnection(string ip)
        {
            this.slaveIp = ip;
            this.slavePort = 502;
            this.slaveUnitId = 1;
            this.connected = false;
        }

        public ModbusConnection(string ip, int port)
        {
            this.slaveIp = ip;
            this.slavePort = port;
            this.slaveUnitId = 1;
            this.connected = false;
        }

        public ModbusConnection(string ip, int port, int unitId)
        {
            this.slaveIp = ip;
            this.slavePort = port;
            this.slaveUnitId = unitId;
            this.connected = false;
        }

        //Modbus connection with slave
        public bool Connected
        {
            get { return this.connected; }
        }

        public void Connect() 
        { 
            IPEndPoint slaveEndpoint = new IPEndPoint(IPAddress.Parse(this.slaveIp), this.slavePort);
            this.slaveSocket = new Socket(IPAddress.Parse(this.slaveIp).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                this.slaveSocket.Connect(slaveEndpoint);
                this.connected = true;
            }
            catch (Exception ex)
            {
                this.connected = false;
            }
        }

        public void Disconnect()
        {
            this.slaveSocket.Shutdown(SocketShutdown.Both);
            this.slaveSocket.Close();
            connected = false;
        }

        //Modbus communication with slave
        public List<bool> ReadCoils(int firstCoilAddress, int rangeOfCoils)
        {
            if (this.connected) 
            {
                byte[] response = new byte[1024];
                int[] values = new int[2];
                values[0] = firstCoilAddress;
                values[1] = rangeOfCoils;

                try
                {
                    byte[] frame = ModBusFrameCreator.CreateFrame(this.slaveUnitId, 1, values);
                    this.slaveSocket.Send(frame);
                    int bytesRecived = this.slaveSocket.Receive(response);
                    Array.Resize(ref response, bytesRecived);
                    ModBusFrameCreator.increaseTransactionId();
                    return new ModbusFrameObject(response).DecodeBits();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public List<bool> ReadDiscreteInputs(int firstInputAddress, int rangeOfDiscreteInputs)
        {
            if (this.connected)
            {
                byte[] response = new byte[1024];
                int[] values = new int[2];
                values[0] = firstInputAddress;
                values[1] = rangeOfDiscreteInputs;

                try
                {
                    byte[] frame = ModBusFrameCreator.CreateFrame(this.slaveUnitId, 2, values);
                    this.slaveSocket.Send(frame);
                    int bytesRecived = this.slaveSocket.Receive(response);
                    Array.Resize(ref response, bytesRecived);
                    ModBusFrameCreator.increaseTransactionId();
                    return new ModbusFrameObject(response).DecodeBits();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public List<int> ReadMultipleHoldingRegisters(int firstRegisterAddress, int rangeOfRegisters)
        {
            if (this.connected)
            {
                byte[] response = new byte[1024];
                int[] values = new int[2];
                values[0] = firstRegisterAddress;
                values[1] = rangeOfRegisters;
                try
                {
                    byte[] frame = ModBusFrameCreator.CreateFrame(this.slaveUnitId, 3, values);
                    this.slaveSocket.Send(frame);
                    int bytesRecived = this.slaveSocket.Receive(response);
                    Array.Resize(ref response, bytesRecived);
                    ModBusFrameCreator.increaseTransactionId();
                    return new ModbusFrameObject(response).DecodeRegisters();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            else return null;
        }

        public List<int> ReadInputRegisters(int firstRegisterAddress, int rangeOfRegisters)
        {
            if (this.connected)
            {
                byte[] response = new byte[1024];
                int[] values = new int[2];
                values[0] = firstRegisterAddress;
                values[1] = rangeOfRegisters;
                try
                {
                    byte[] frame = ModBusFrameCreator.CreateFrame(this.slaveUnitId, 4, values);
                    this.slaveSocket.Send(frame);
                    int bytesRecived = this.slaveSocket.Receive(response);
                    Array.Resize(ref response, bytesRecived);
                    ModBusFrameCreator.increaseTransactionId();
                    return new ModbusFrameObject(response).DecodeRegisters();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            else return null;
        }

        public bool WriteSingleCoil(int coilAddress, int coilValue)
        {
            if (this.connected)
            {
                byte[] response = new byte[1024];
                int[] values = new int[2];
                values[0] = coilAddress;
                values[1] = coilValue;
                try
                {
                    byte[] frame = ModBusFrameCreator.CreateFrame(this.slaveUnitId, 5, values);
                    this.slaveSocket.Send(frame);
                    int bytesRecived = this.slaveSocket.Receive(response);
                    Array.Resize(ref response, bytesRecived);
                    ModBusFrameCreator.increaseTransactionId();
                    return frame.SequenceEqual(response);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else return false;
        }

        public bool WriteSingleHoldingRegister(int holdingRegisterAddress, int holdingRegisterValue)
        {
            if (this.connected)
            {
                byte[] response = new byte[1024];
                int[] values = new int[2];
                values[0] = holdingRegisterAddress;
                values[1] = holdingRegisterValue;
                try
                {
                    byte[] frame = ModBusFrameCreator.CreateFrame(this.slaveUnitId, 6, values);
                    this.slaveSocket.Send(frame);
                    int bytesRecived = this.slaveSocket.Receive(response);
                    Array.Resize(ref response, bytesRecived);
                    ModBusFrameCreator.increaseTransactionId();
                    return frame.SequenceEqual(response);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else return false;
        }

        //Coil value can be only 0 = ON or 1 = OFF
        public bool WriteMultipleCoils(int startCoilAddress, int rangeOfCoils, int[] coilsValues)
        {
            if (this.connected)
            {
                byte[] response = new byte[1024];
                int[] values = new int[2 + coilsValues.Length];
                values[0] = startCoilAddress;
                values[1] = rangeOfCoils;
                for(int i = 2; i < values.Length; i++)
                {
                    values[i] = coilsValues[i - 2];
                }
                try
                {
                    byte[] frame = ModBusFrameCreator.CreateFrame(this.slaveUnitId, 15, values);
                    this.slaveSocket.Send(frame);
                    int bytesRecived = this.slaveSocket.Receive(response);
                    Array.Resize(ref response, bytesRecived);
                    ModBusFrameCreator.increaseTransactionId();
                    Array.Resize(ref frame, 12);
                    frame[5] = 0x06;
                    return frame.SequenceEqual(response);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else return false;
        }

        public bool WriteMultipleHoldingRegisters(int startRegisterAddress, int rangeOfRegisters, int[] registersValues)
        {
            if (this.connected)
            {
                byte[] response = new byte[1024];
                int[] values = new int[2 + registersValues.Length];
                values[0] = startRegisterAddress;
                values[1] = rangeOfRegisters;
                for (int i = 2; i < values.Length; i++)
                {
                    values[i] = registersValues[i - 2];
                }
                try
                {
                    byte[] frame = ModBusFrameCreator.CreateFrame(this.slaveUnitId, 16, values);
                    this.slaveSocket.Send(frame);
                    int bytesRecived = this.slaveSocket.Receive(response);
                    Array.Resize(ref response, bytesRecived);
                    ModBusFrameCreator.increaseTransactionId();
                    Array.Resize(ref frame, 12);
                    frame[5] = 0x06;
                    return frame.SequenceEqual(response);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else return false;
        }



        //Utility
        public void ReadFrame(byte[] frame)
        {
            foreach (byte b in frame)
            {
                Console.Write(b.ToString("x2") + " ");
            }
            Console.WriteLine();
        }


    }
}
