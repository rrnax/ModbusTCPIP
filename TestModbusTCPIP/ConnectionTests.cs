using ModbusTCPIP;

namespace TestModbusTCPIP
{
    public class ConnectionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        //Slave must be active to test that tests
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
        public void Check_ReadCoils()
        {
            //Asign
            ModbusConnection slave = new ModbusConnection();
            int address = 0;
            int range = 10;
            List<bool> expected = new List<bool>{ true, false, true, false, false, false, false, true, true, true };
            List<bool> result;

            //Act
            slave.Connect();
            result = slave.ReadCoils(address, range);
            slave.Disconnect();

            //Assert
            CollectionAssert.AreEqual(result, expected);
        }

        [Test]
        public void Check_ReadCoils_outRange()
        {
            //Asign
            ModbusConnection slave = new ModbusConnection();
            int address = 0;
            int range = 3000;
            List<bool> expected = null;
            List<bool> result;

            //Act
            slave.Connect();
            result = slave.ReadCoils(address, range);
            slave.Disconnect();

            //Assert
            CollectionAssert.AreEqual(result, expected);
        }

        [Test]
        public void Check_ReadDiscreteInputs()
        {
            //Asign
            ModbusConnection slave = new ModbusConnection();
            int address = 0;
            int range = 10;
            List<bool> expected = new List<bool> {false, true, true, true, false, false, false, true, true, true };
            List<bool> result;

            //Act
            slave.Connect();
            result = slave.ReadDiscreteInputs(address, range);
            slave.Disconnect();

            //Assert
            CollectionAssert.AreEqual(result, expected);
        }

        [Test]
        public void Check_ReadMultipleHoldingRegisters()
        {
            //Asign
            ModbusConnection slave = new ModbusConnection();
            int address = 0;
            int range = 10;
            List<int> expected = new List<int> { 321, 534, 0,435, 44, 34, 0, 0, 0, 534 };
            List<int> result;

            //Act
            slave.Connect();
            result = slave.ReadMultipleHoldingRegisters(address, range);
            slave.Disconnect();

            //Assert
            CollectionAssert.AreEqual(result, expected);
        }

        [Test]
        public void Check_ReadMultipleHoldingRegisters_outRange()
        {
            //Asign
            ModbusConnection slave = new ModbusConnection();
            int address = 0;
            int range = 70000;
            List<int> expected = null;
            List<int> result;

            //Act
            slave.Connect();
            result = slave.ReadMultipleHoldingRegisters(address, range);
            slave.Disconnect();

            //Assert
            CollectionAssert.AreEqual(result, expected);
        }

        [Test]
        public void Check_ReadInputRegisters()
        {
            //Asign
            ModbusConnection slave = new ModbusConnection();
            int address = 0;
            int range = 10;
            List<int> expected = new List<int> { 3232, 4, 333, 2123, 43, 4444, 3, 0, 0, 0 };
            List<int> result;

            //Act
            slave.Connect();
            result = slave.ReadInputRegisters(address, range);
            slave.Disconnect();

            //Assert
            CollectionAssert.AreEqual(result, expected);
        }

        [Test]
        public void Check_ReadInputRegisters_outRange()
        {
            //Asign
            ModbusConnection slave = new ModbusConnection();
            int address = 0;
            int range = 200;
            List<int> expected = null;
            List<int> result;

            //Act
            slave.Connect();
            result = slave.ReadInputRegisters(address, range);
            slave.Disconnect();

            //Assert
            CollectionAssert.AreEqual(result, expected);
        }
    }
}
