using System.Runtime.Serialization;

namespace VendingMachine.App.Exceptions
{
    [Serializable]
    public class InsuficientBalanceException : Exception
    {
        static string _message = "The balance is less than the product price. Cancel operation.";

        public InsuficientBalanceException() : base(message: _message)
        {
        }

        public InsuficientBalanceException(string? message) : base(message)
        {
        }

        public InsuficientBalanceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InsuficientBalanceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
