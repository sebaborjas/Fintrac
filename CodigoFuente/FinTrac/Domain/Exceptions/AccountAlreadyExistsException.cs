using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class AccountAlreadyExistsException : Exception
    {
        public static string Message = "La cuenta ya se encuentra registrada";

        public AccountAlreadyExistsException() : this(Message)
        {

        }

        private AccountAlreadyExistsException(string? message) : base(message)
        {

        }

    }
}