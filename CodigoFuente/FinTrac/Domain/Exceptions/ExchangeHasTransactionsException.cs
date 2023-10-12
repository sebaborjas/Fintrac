using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class ExchangeHasTransactionsException : Exception
    {
        public static string Message = "No se puede eliminar el cambio ya que existe transacciones que lo requieren";

        public ExchangeHasTransactionsException() : this(Message)
        {

        }

        private ExchangeHasTransactionsException(string? message) : base(message)
        {

        }

    }
}