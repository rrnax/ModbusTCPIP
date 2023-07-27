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
            List<bool> expected = new List<bool>{ true, false, true, false, false, false, false, true, true, true };//List Coils from your slave
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
            List<bool> expected = new List<bool> {false, true, true, true, false, false, false, true, true, true };//List discrete inputs from your slave
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
            List<int> expected = new List<int> { 321, 534, 0,435, 44, 34, 0, 0, 0, 534 }; //List register from your slave
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
            List<int> expected = new List<int> { 3232, 4, 333, 2123, 43, 4444, 3, 0, 0, 0 };//List Input register from your slave
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

        [Test]
        public void Check_WriteSingleCoilON()
        {
            //Asign
            ModbusConnection slave = new ModbusConnection();
            int address = 100;
            int value = 65280;
            bool result;

            //Act
            slave.Connect();
            result = slave.WriteSingleCoil(address, value);
            slave.Disconnect();

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Check_WriteSingleCoilOFF()
        {
            //Asign
            ModbusConnection slave = new ModbusConnection();
            int address = 100;
            int value = 0;
            bool result;

            //Act
            slave.Connect();
            result = slave.WriteSingleCoil(address, value);
            slave.Disconnect();

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Check_WriteSingleCoilIncorrect()
        {
            //Asign
            ModbusConnection slave = new ModbusConnection();
            int address = 2;
            int value = 2323;
            bool result;

            //Act
            slave.Connect();
            result = slave.WriteSingleCoil(address, value);
            slave.Disconnect();

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Check_WriteSingleRegister()
        {
            //Asign
            ModbusConnection slave = new ModbusConnection();
            int address = 12;
            int value = 21280;
            bool result;

            //Act
            slave.Connect();
            result = slave.WriteSingleHoldingRegister(address, value);
            slave.Disconnect();

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Check_WriteSingleRegisterIncorrect()
        {
            //Asign
            ModbusConnection slave = new ModbusConnection();
            int address = -12;
            int value = 21280;
            bool result;

            //Act
            slave.Connect();
            result = slave.WriteSingleHoldingRegister(address, value);
            slave.Disconnect();

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Test_WriteMultipleCoils1() 
        {
            //Asign
            ModbusConnection slave = new ModbusConnection();
            int address = 3;
            int range = 7;
            int[] vals = { 1, 1, 1, 1, 0, 0, 1 };
            bool result;

            //Act
            slave.Connect();
            result = slave.WriteMultipleCoils(address, range, vals);
            slave.Disconnect();

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_WriteMultipleCoilsInncorect()
        {
            //Asign
            ModbusConnection slave = new ModbusConnection();
            int address = 0;
            int range = 10;
            int[] vals = { 0, 0, 0, 0, 1, 1, 1 , 0, 1};
            bool result;

            //Act
            slave.Connect();
            result = slave.WriteMultipleCoils(address, range, vals);
            slave.Disconnect();

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Test_WriteMultipleCoils2()
        {
            //Asign
            ModbusConnection slave = new ModbusConnection();
            int address = 0;
            int range = 10;
            int[] vals = { 0, 0, 0, 0, 1, 1, 1, 0, 1 ,1};
            bool result;

            //Act
            slave.Connect();
            result = slave.WriteMultipleCoils(address, range, vals);
            slave.Disconnect();

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_WriteMultipleRegisters1()
        {
            //Asign
            ModbusConnection slave = new ModbusConnection();
            int address = 3;
            int range = 7;
            int[] vals = { 211, 3331, 31, 1, 0, 1012, 22221 };
            bool result;

            //Act
            slave.Connect();
            result = slave.WriteMultipleHoldingRegisters(address, range, vals);
            slave.Disconnect();

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_WriteMultipleRegistersIncorrect()
        {
            //Asign
            ModbusConnection slave = new ModbusConnection();
            int address = 3;
            int range = 72;
            int[] vals = { 211, 3331, 31, 1, 0, 1012, 22221 };
            bool result;

            //Act
            slave.Connect();
            result = slave.WriteMultipleHoldingRegisters(address, range, vals);
            slave.Disconnect();

            //Assert
            Assert.IsFalse(result);
        }
    }
}
