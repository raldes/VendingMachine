using System.Runtime.Serialization;

namespace VendingMachine.App.Exceptions
{
    [Serializable]
    internal class MachineStateException : Exception
    {
        public MachineStateException()
        {
        }

        public MachineStateException(string? message) : base(message)
        {
        }

        public MachineStateException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected MachineStateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
