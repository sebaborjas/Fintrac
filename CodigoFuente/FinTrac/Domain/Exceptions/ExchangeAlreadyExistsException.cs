using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class ExchangeAlreadyExistsException : Exception
    {
        public static string Message = "Ya se registró un camio para esa fecha";

        public ExchangeAlreadyExistsException() : this(Message)
        {

        }

        private ExchangeAlreadyExistsException(string? message) : base(message)
        {

        }

    }
}