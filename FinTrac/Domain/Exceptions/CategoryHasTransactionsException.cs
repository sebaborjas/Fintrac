using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class CategoryHasTransactionsException : Exception
    {
        public static string Message = "No se puede eliminar la categoría ya que existe transacciones que la utilizan";

        public CategoryHasTransactionsException() : this(Message)
        {

        }

        private CategoryHasTransactionsException(string? message) : base(message)
        {

        }

    }
}