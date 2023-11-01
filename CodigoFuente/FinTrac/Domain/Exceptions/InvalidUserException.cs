using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class InvalidUserException : Exception
    {
        public static string Message = "Usuario Incorrecto";

        public InvalidUserException() : this(Message)
        {

        }

        private InvalidUserException(string? message) : base(message)
        {

        }
    }
}