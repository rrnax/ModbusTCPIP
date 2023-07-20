namespace TestModbusTCPIP
{
    public class ModbusFrameTests
    {
        [SetUp]
        public void Setup()
        {
        }

        //Check creating a complete frame
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Test]
        public void CheckThrowinException_inSelectFunction()
        {
            //Asign
            int functionId = 300;
            int unitId = 1;
            int[] sampleData = { 1, 2 };

            //Act with Assert
            Assert.Throws<FrameException>(() =>  ModBusFrameCreator.CreateFrame(unitId, functionId, sampleData));
        }

        [Test]
        public void CheckCase1()
        {
            //Asign
            int functionId = 1;
            int unitId = 1;
            int[] sampleData = { 1, 300 };
            byte[] expectedFrame = { 0, 0, 0, 0, 0, 6, 1, 1, 0, 1, 0x01, 0x2c };

            //Act
            byte[] actualFrame = ModBusFrameCreator.CreateFrame(unitId, functionId, sampleData);

            //Assert
            CollectionAssert.AreEqual(expectedFrame, actualFrame);
        }

        [Test]
        public void CheckCase2()
        {
            //Asign
            int functionId = 2;
            int unitId = 1;
            int[] sampleData = { 1, 300 };
            byte[] expectedFrame = { 0, 0, 0, 0, 0, 6, 1, 2, 0, 1, 0x01, 0x2c };

            //Act
            byte[] actualFrame = ModBusFrameCreator.CreateFrame(unitId, functionId, sampleData);

            //Assert
            CollectionAssert.AreEqual(expectedFrame, actualFrame);
        }

        [Test]
        public void CheckCase3()
        {
            //Asign
            int functionId = 3;
            int unitId = 1;
            int[] sampleData = { 1, 300 };
            byte[] expectedFrame = { 0, 0, 0, 0, 0, 6, 1, 3, 0, 1, 0x01, 0x2c };

            //Act
            byte[] actualFrame =  ModBusFrameCreator.CreateFrame(unitId, functionId, sampleData);

            //Assert
            CollectionAssert.AreEqual(expectedFrame, actualFrame);
        }

        [Test]
        public void CheckCase4()
        {
            //Asign
            int functionId = 4;
            int unitId = 1;
            int[] sampleData = { 1, 100 };
            byte[] expectedFrame = { 0, 0, 0, 0, 0, 6, 1, 4, 0, 1, 0, 100 };

            //Act
            byte[] actualFrame = ModBusFrameCreator.CreateFrame(unitId, functionId, sampleData);

            //Assert
            CollectionAssert.AreEqual(expectedFrame, actualFrame);
        }

        [Test]
        public void CheckCase5()
        {
            //Asign
            int functionId = 5;
            int unitId = 1;
            int[] sampleData = { 1, 0 };
            byte[] expectedFrame = { 0, 0, 0, 0, 0, 6, 1, 5, 0, 1, 0, 0 };

            //Act
            byte[] actualFrame = ModBusFrameCreator.CreateFrame(unitId, functionId, sampleData);

            //Assert
            CollectionAssert.AreEqual(expectedFrame, actualFrame);
        }


        [Test]
        public void CheckCase6()
        {
            //Asign
            int functionId = 6;
            int unitId = 1;
            int[] sampleData = { 1, 300 };
            byte[] expectedFrame = { 0, 0, 0, 0, 0, 6, 1, 6, 0, 1, 0x01, 0x2c };

            //Act
            byte[] actualFrame =  ModBusFrameCreator.CreateFrame(unitId, functionId, sampleData);

            //Assert
            CollectionAssert.AreEqual(expectedFrame, actualFrame);
        }

        [Test]
        public void CheckBadScenarioForCase16()
        {
            //Asign
            int functionId = 16;
            int unitId = 1;
            int[] sampleData = { 1, 2, 70000, 333 };

            //Act with Assert
            Assert.Throws<FrameException>(() =>  ModBusFrameCreator.CreateFrame(unitId, functionId, sampleData));
        }

        [Test]
        public void CheckGoodScenarioForCase16()
        {
            //Asign
            int functionId = 16;
            int unitId = 1;
            int[] sampleData = { 1, 2, 300, 21 };
            byte[] expectedFrame = { 0, 1, 0, 0, 0, 11, 1, 16, 0, 1, 0, 2, 4, 0x01, 0x2c, 0, 0x15 };

            //Act
            byte[] actualFrame =  ModBusFrameCreator.CreateFrame(unitId, functionId, sampleData);

            //Assert
            CollectionAssert.AreEqual(expectedFrame, actualFrame);
        }
        //Decode Modbus Frame
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Test]
        public void CheckDecode_CorrectFrame()
        {
            //Asign
            byte[] frame = { 0, 0, 0, 0, 0, 13, 1, 3, 10, 0x01, 0x16, 0, 0x21, 0, 0x64, 0x27, 0x24, 0, 0x63 };
            List<int> expected = new List<int> { 278, 33, 100, 10020, 99 };

            //Act
            List<int> result =  ModBusFrameCreator.DecodeModbusFrame(frame);

            //Asserts
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void CheckDecode_InCorrectFrame()
        {
            //Asign
            byte[] frame = { 0, 0, 0, 0, 0, 13 };
            List<int> expected = new List<int> { 278, 33, 100, 10020, 99 };

            //Act and Assert
            Assert.Throws<FrameException>(() =>  ModBusFrameCreator.DecodeModbusFrame(frame));
        }

        //PDU for Reading
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        [Test]
        public void CheckPDURead_CorrectValues()
        {
            //Asign
            int startCoil = 1;
            int range = 2000;
            int functionCode = 2;
            byte[] expectedFrame = { 2, 0, 1, 0x07, 0xd0 };

            //Act
            byte[] actualFrame = ModBusFrameCreator.ReadingPDU(startCoil, range, functionCode);

            //Assert
            CollectionAssert.AreEqual(expectedFrame, actualFrame);
        }

        [Test]
        public void CheckPDURead_RangeOver2000()
        {
            //Asign
            int startCoil = 1;
            int range = 2001;
            int functionCode = 2;

            //Act and Assert
            Assert.Throws<FrameException>(() => ModBusFrameCreator.ReadingPDU(startCoil, range, functionCode));
        }

        [Test]
        public void CheckPDURead_RangeOver125AndFunctionCode4()
        {
            //Asign
            int startCoil = 1;
            int range = 160;
            int functionCode = 4;

            //Act and Assert
            Assert.Throws<FrameException>(() => ModBusFrameCreator.ReadingPDU(startCoil, range, functionCode));
        }

        [Test]
        public void CheckPDURead_RangeUnder1()
        {
            //Asign
            int startCoil = 1;
            int range = 0;
            int functionCode = 2;

            //Act and Assert
            Assert.Throws<FrameException>(() => ModBusFrameCreator.ReadingPDU(startCoil, range, functionCode));
        }


        [Test]
        public void CheckPDURead_SumOfRangeAndCoil()
        {
            //Asign
            int startCoil = 65000;
            int range = 2000;
            int functionCode = 2;

            //Act and Assert
            Assert.Throws<FrameException>(() => ModBusFrameCreator.ReadingPDU(startCoil, range, functionCode));
        }


      

        //PDU for writing single
        //////////////////////////////////////////////////////////////////////////////////////////////////
        [Test]
        public void CheckPDUWriteSingle_Correct()
        {
            //Asign
            int adress = 1;
            int value = 65535;
            byte[] expectatedFrame = { 6, 0, 1, 0xff, 0xff };

            //Act
            byte[] actualFrame =  ModBusFrameCreator.SingleWritingPDU(adress, value, 6);

            //Assert
            CollectionAssert.AreEqual(expectatedFrame, actualFrame);

        }

        

        [Test]
        public void CheckPDUWriteSingle_IncorrectValueIn5Function()
        {
            //Asign
            int adress = 1;
            int value = 200;

            //Act and Assert
            Assert.Throws<FrameException>(() =>  ModBusFrameCreator.SingleWritingPDU(adress, value, 5));

        }
        //Test for writing multiple registers
        //////////////////////////////////////////////////////////////////////////////////////////////////
        [Test]
        public void CheckPDUForWriteMultipleHoldingRegister_Correct_difrretSizeOfbytes()
        {
            //Asign
            int start = 1;
            int range = 5;
            int[] values = { 13, 33, 345, 21, 65000 };
            byte[] expectatedFrame = { 16, 0, 1, 0, 5, 10, 0, 13, 0, 33, 0x01, 0x59, 0, 21, 0xfd, 0xe8 };

            //Act
            byte[] actualFrame =  ModBusFrameCreator.WritingMultipleHoldingRegistersPDU(start, range, values);

            //Assert
            CollectionAssert.AreEqual(expectatedFrame, actualFrame);
        }

        [Test]
        public void CheckPDUForWriteMultipleHoldingRegister_Correct_lowBytesOnly()
        {
            //Asign
            int start = 1;
            int range = 5;
            int[] values = { 13, 33, 222, 21, 1 };
            byte[] expectatedFrame = { 16, 0, 1, 0, 5, 10, 0, 13, 0, 33, 0, 222, 0, 21, 0, 1 };

            //Act
            byte[] actualFrame =  ModBusFrameCreator.WritingMultipleHoldingRegistersPDU(start, range, values);

            //Assert
            CollectionAssert.AreEqual(expectatedFrame, actualFrame);
        }

        [Test]
        public void CheckPDUForWriteMultipleHoldingRegister_Correct_HighBytesOnly()
        {
            //Asign
            int start = 1;
            int range = 5;
            int[] values = { 300, 452, 7000, 4532, 12000 };
            byte[] expectatedFrame = { 16, 0, 1, 0, 5, 10, 0x01, 0x2c, 0x01, 0xc4, 0x1b, 0x58, 0x11, 0xb4, 0x2e, 0xe0 };

            //Act
            byte[] actualFrame =  ModBusFrameCreator.WritingMultipleHoldingRegistersPDU(start, range, values);

            //Assert
            CollectionAssert.AreEqual(expectatedFrame, actualFrame);
        }

        [Test]
        public void CheckPDUForWriteMultipleHoldingRegister_IncorrrectLengths()
        {
            //Asign
            int start = 1;
            int range = 7;
            int[] values = { 300, 452, 7000, 4532, 12000 };

            //Act and Assert
            Assert.Throws<FrameException>(() =>  ModBusFrameCreator.WritingMultipleHoldingRegistersPDU(start, range, values));
        }

        [Test]
        public void CheckPDUForWriteMultipleHoldingRegister_IncorrrectValuesOutOfRange()
        {
            //Asign
            int start = 1;
            int range = 5;
            int[] values = { 300, 452, 70000, 4532, 120000 };

            //Act and Assert
            Assert.Throws<FrameException>(() =>  ModBusFrameCreator.WritingMultipleHoldingRegistersPDU(start, range, values));
        }

        [Test]
        public void CheckPDUForWriteMultipleHoldingRegister_IncorrrectRegister()
        {
            //Asign
            int start = -122;
            int range = 5;
            int[] values = { 300, 452, 70000, 4532, 120000 };

            //Act and Assert
            Assert.Throws<FrameException>(() =>  ModBusFrameCreator.WritingMultipleHoldingRegistersPDU(start, range, values));
        }

        [Test]
        public void CheckPDUForWriteMultipleHoldingRegister_IncorrrectRegister2()
        {
            //Asign
            int start = 122122;
            int range = 5;
            int[] values = { 300, 452, 70000, 4532, 120000 };

            //Act and Assert
            Assert.Throws<FrameException>(() =>  ModBusFrameCreator.WritingMultipleHoldingRegistersPDU(start, range, values));
        }

        [Test]
        public void CheckPDUForWriteMultipleHoldingRegister_IncorrrectRange()
        {
            //Asign
            int start = 1;
            int range = 222225;
            int[] values = { 300, 452, 70000, 4532, 120000 };

            //Act and Assert
            Assert.Throws<FrameException>(() =>  ModBusFrameCreator.WritingMultipleHoldingRegistersPDU(start, range, values));
        }


        [Test]
        public void CheckPDUForWriteMultipleHoldingRegister_IncorrrectRange2()
        {
            //Asign
            int start = 1;
            int range = -222;
            int[] values = { 300, 452, 70000, 4532, 120000 };

            //Act and Assert
            Assert.Throws<FrameException>(() =>  ModBusFrameCreator.WritingMultipleHoldingRegistersPDU(start, range, values));
        }

        //Create Header Section of protocol
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [Test]
        public void CheckMBAPHeader_Correct()
        {
            //Asign
            int unitId = 1;
            int length = 5;
            byte[] expectatedFrame = { 0, 1, 0, 0, 0, 6, 1 };

            //Act
            byte[] actualFrame =  ModBusFrameCreator.CreateMBAPHeader(unitId, length);

            //Assert
            CollectionAssert.AreEqual(expectatedFrame, actualFrame);
        }

        [Test]
        public void CheckMBAPHeader_CorrectMaxLength()
        {
            //Asign
            int unitId = 1;
            int length = 65534;
            byte[] expectatedFrame = { 0, 1, 0, 0, 0xff, 0xff, 1 };

            //Act
            byte[] actualFrame =  ModBusFrameCreator.CreateMBAPHeader(unitId, length);

            //Assert
            CollectionAssert.AreEqual(expectatedFrame, actualFrame);
        }

        [Test]
        public void CheckMBAPHeader_InCorrectMaxLength()
        {
            //Asign
            int unitId = 1;
            int length = 65535;

            //Act and Assert
            Assert.Throws<FrameException>(() =>  ModBusFrameCreator.CreateMBAPHeader(unitId, length));
        }

        [Test]
        public void CheckMBAPHeader_InCorrectUnitID()
        {
            //Asign
            int unitId = 256;
            int length = 20;

            //Act and Assert
            Assert.Throws<FrameException>(() => ModBusFrameCreator.CreateMBAPHeader(unitId, length));
        }

        [Test]
        public void CheckMBAPHeader_InCorrectUnitID2()
        {
            //Asign
            int unitId = -30;
            int length = 20;

            //Act and Assert
            Assert.Throws<FrameException>(() => ModBusFrameCreator.CreateMBAPHeader(unitId, length));
        }

        [Test]
        public void CheckMBAPHeader_InCorrectNegativeLength()
        {
            //Asign
            int unitId = 30;
            int length = -20;

            //Act and Assert
            Assert.Throws<FrameException>(() => ModBusFrameCreator.CreateMBAPHeader(unitId, length));
        }
    }
}