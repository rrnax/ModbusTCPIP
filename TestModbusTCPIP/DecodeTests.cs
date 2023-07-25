namespace TestModbusTCPIP
{
    public class DecodeTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckConnection()
        {
            //Asign
            byte[] frame = { 0, 0, 0, 0, 0, 5, 1, 1, 2, 0x91, 1 };
            List<bool> expected = new List<bool> { true, false, false, false, true, false, false, true, true, false};

            //Act
            ModbusDecodeFrames actual = new ModbusDecodeFrames(frame);

            //Assert
            CollectionAssert.AreEqual(expected, actual.DecodeCoils());  
        }
    }
}
