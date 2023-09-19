using System.Runtime.Serialization;

namespace Domain
{
    [Serializable]
    public class EmptyFieldException : Exception
    {
        public static string Message = "No se admiten campos vacios";

        public EmptyFieldException() : this(Message){

        }
 
        private EmptyFieldException(string? message) : base(message)
        {

        }

    }
}