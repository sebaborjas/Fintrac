using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class CategoryAlreadyExistsException : Exception
    {
        public static string Message = "Ya se existe una categoría con ese nombre";

        public CategoryAlreadyExistsException() : this(Message)
        {

        }

        private CategoryAlreadyExistsException(string? message) : base(message)
        {

        }

    }
}