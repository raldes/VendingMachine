using System.Runtime.Serialization;

namespace VendingMachine.App.Exceptions
{
    [Serializable]
    internal class MachineException : Exception
    {
        public MachineException()
        {
        }

        public MachineException(string? message) : base(message)
        {
        }

        public MachineException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected MachineException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
