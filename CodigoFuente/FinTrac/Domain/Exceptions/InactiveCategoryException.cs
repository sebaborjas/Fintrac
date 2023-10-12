using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class InactiveCategoryException : Exception
    {
        public static string Message = "La categoria esta inactiva";

        public InactiveCategoryException() : this(Message)
        {

        }

        private InactiveCategoryException(string? message) : base(message)
        {

        }
    }
}