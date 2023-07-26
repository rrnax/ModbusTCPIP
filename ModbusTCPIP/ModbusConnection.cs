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
        private bool connected;


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
            else
            {
                return null;
            }
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
            else
            {
                return null;
            }
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
