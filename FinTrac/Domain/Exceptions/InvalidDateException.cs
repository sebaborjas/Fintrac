using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class InvalidDateException : Exception
    {
        public static string Message = "La primer fecha debe ser menor o igual a segunda";

        public InvalidDateException() : this(Message)
        {

        }

        private InvalidDateException(string? message) : base(message)
        {

        }
    }
}