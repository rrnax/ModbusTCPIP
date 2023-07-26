namespace TestModbusTCPIP
{
    public class DecodeTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckDecodeCoils()
        {
            //Asign
            byte[] frame = { 0, 0, 0, 0, 0, 5, 1, 1, 2, 0x35, 3 };
            List<bool> expected = new List<bool> { true, false, true, false, true, true, false, false, true, true};

            //Act
            ModbusFrameObject actual = new ModbusFrameObject(frame);

            //Assert
            CollectionAssert.AreEqual(expected, actual.DecodeBits());  
        }

        [Test]
        public void CheckDecodeCoils_incorectFunction()
        {
            //Asign
            byte[] frame = { 0, 0, 0, 0, 0, 5, 1, 4, 2, 0x35, 3 };
            List<bool> expected = new List<bool> { true, false, true, false, true, true, false, false, true, true };

            //Act
            ModbusFrameObject actual = new ModbusFrameObject(frame);

            //Assert
            Assert.Throws<FrameException>(() => actual.DecodeBits());
        }

        [Test]
        public void CheckDecodeRegister()
        {
            //Asign
            byte[] frame = { 0, 0, 0, 0, 0, 17, 1, 4, 0x14, 1, 0x57, 1, 0x44, 0, 0x17, 0, 0x36, 0, 0xea, 0x56, 0xce, 0, 0, 0x0c, 0xab, 1, 0xa7, 0, 0 };
            List<int> expected = new List<int> {343, 324, 23, 54, 234, 22222, 0, 3243, 423, 0};

            //Act
            ModbusFrameObject actual = new ModbusFrameObject(frame);

            //Assert
            CollectionAssert.AreEqual(expected, actual.DecodeRegisters());
        }


        [Test]
        public void CheckDecodeRegister_incorrectFunction()
        {
            //Asign
            byte[] frame = { 0, 0, 0, 0, 0, 17, 1, 14, 0x14, 1, 0x57, 1, 0x44, 0, 0x17, 0, 0x36, 0, 0xea, 0x56, 0xce, 0, 0, 0x0c, 0xab, 1, 0xa7, 0, 0 };
            List<int> expected = new List<int> { 343, 324, 23, 54, 234, 22222, 0, 3243, 423, 0 };

            //Act
            ModbusFrameObject actual = new ModbusFrameObject(frame);

            //Assert
            Assert.Throws<FrameException>(() => actual.DecodeRegisters());
        }
    }
}
