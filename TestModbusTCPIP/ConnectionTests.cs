using ModbusTCPIP;

namespace TestModbusTCPIP
{
    public class ConnectionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckConnection()
        {
            //Asign
            ModbusConnection slave = new ModbusConnection();

            //Act
            slave.Connect();

            //Assert
            Assert.IsTrue(slave.Connected);
        }

        [Test]
        public void CheckConnection_CannotConnect()
        {
            //Asign
            ModbusConnection slave = new ModbusConnection("192.0.0.3");

            //Act
            slave.Connect();

            //Assert
            Assert.IsFalse(slave.Connected);
        }

        [Test]
        public void CheckConnection_Disconnect()
        {
            //Asign
            ModbusConnection slave = new ModbusConnection();

            //Act
            slave.Connect();
            slave.Disconnect();

            //Assert
            Assert.IsFalse(slave.Connected);
        }

        [Test]
        public void CheckConnection_ReadCoils()
        {
            //Asign
            ModbusConnection slave = new ModbusConnection();
            int address = 1;
            int range = 10;
            byte[] expected = { 0, 0, 0, 0, 0, 5, 1, 1, 2, 0x91, 1 };
            byte[] result;

            //Act
            slave.Connect();
            result = slave.ReadCoils(address, range);
            slave.Disconnect();

            //Assert
            CollectionAssert.AreEqual(result, expected);
        }
    }
}
