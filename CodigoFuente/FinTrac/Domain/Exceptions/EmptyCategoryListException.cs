using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class EmptyCategoryListException : Exception
    {

        public static string Message = "Debe seleccionar al menos una categoría";


        public EmptyCategoryListException() : this(Message)
        {

        }

        private EmptyCategoryListException(string? message) : base(message)
        {

        }

    }
}