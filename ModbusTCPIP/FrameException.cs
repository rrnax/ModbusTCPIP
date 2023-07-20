
namespace ModbusTCPIP
{
    public class FrameException : Exception
    {
        public string message { get; }

        public FrameException(string message) : base(message)
        {
            this.message = message;
        }
    }
}
