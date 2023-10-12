using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class TransactionAlreadyExistsException : Exception
    {
        public static string Message = "Ya se registró esa transacción";

        public TransactionAlreadyExistsException() : this(Message)
        {

        }

        private TransactionAlreadyExistsException(string? message) : base(message)
        {

        }

    }
}