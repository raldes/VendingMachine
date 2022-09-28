using System.Runtime.Serialization;

namespace VendingMachine.App.Exceptions
{
    [Serializable]
    public class ChangeHaveNoSolutionException : Exception
    {
        static string _message = "There are not enough coins for change. Cancel operation.";

        public ChangeHaveNoSolutionException() : base(message: _message) 
        {
        }

        public ChangeHaveNoSolutionException(string? message) : base(message)
        {
        }

        public ChangeHaveNoSolutionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ChangeHaveNoSolutionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
