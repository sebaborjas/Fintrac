using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class ExchangeNotFoundException : Exception
    {
        public static string Message = "Para ingresar una transacción, debe ingresar al menos un tipo de cambio anterior a la fehca de la transacción";

        public ExchangeNotFoundException() : this(Message)
        {

        }

        private ExchangeNotFoundException(string? message) : base(message)
        {

        }

    }
}