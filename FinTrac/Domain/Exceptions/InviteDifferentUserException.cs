using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class InviteDifferentUserException : Exception
    {
        public static string Message = "No se puede enviar una invitación ústed mismo";

        public InviteDifferentUserException() : this(Message)
        {

        }

        private InviteDifferentUserException(string? message) : base(message)
        {

        }
    }
}