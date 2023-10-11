using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class InvalidTransactionCurrencyException : Exception
    {
        public static string Message = "El tipo de moneda de la transaccion debe ser igual a el de la cuenta";

        public InvalidTransactionCurrencyException() : this(Message)
        {

        }

        private InvalidTransactionCurrencyException(string? message) : base(message)
        {

        }

    }
}