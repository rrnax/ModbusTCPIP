namespace ModbusTCPIP
{
    public class ModbusDecodeFrames
    {
        public byte[] orginalFrame { get; }
        public int transactionId { get; }
        public int protocolId { get; }
        public int amountBytesOfPDU { get; }
        public int unitId { get; }  
        public int functionCode { get; }

        //Initialize and read Protocol Header
        public ModbusDecodeFrames(byte[] frame)
        {
            this.orginalFrame = frame;
            this.transactionId = this.CombineBytes(frame[0], frame[1]);
            this.protocolId = this.CombineBytes(frame[2], frame[3]);
            this.amountBytesOfPDU = this.CombineBytes(frame[4], frame[5]);  //Protocol Data Unit Length
            this.unitId = Convert.ToInt32(frame[6]);
            this.functionCode = Convert.ToInt32(frame[7]);
        }
        
        //Decode Coils
        public List<bool> DecodeCoils()
        {
            List<bool> result = new List<bool>();
            int bytesAmount = Convert.ToInt32(this.orginalFrame[8]);
            if (this.functionCode == 1)
            {
                string reversCoils = "";
                for(int i = 9; i < this.orginalFrame.Length; i++)
                {
                    reversCoils += Convert.ToString(this.orginalFrame[i], 2).Reverse().ToArray();
                }
                foreach(char bit in reversCoils)
                {
                    if (bit == 0) result.Add(false);
                    else result.Add(true);
                }
                return result;
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
