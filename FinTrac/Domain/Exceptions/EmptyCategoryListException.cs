using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class EmptyCategoryListException : Exception
    {
        public static string Message = "No se admiten campos vacios";

        public EmptyCategoryListException() : this(Message)
        {

        }

        private EmptyCategoryListException(string? message) : base(message)
        {

        }

    }
}