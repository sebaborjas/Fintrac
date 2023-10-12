using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class InvitationAlreadyExistsException : Exception
    {
        public static string Message = "Esa invitación ya fue enviada previamente";

        public InvitationAlreadyExistsException() : this(Message)
        {

        }

        private InvitationAlreadyExistsException(string? message) : base(message)
        {

        }

    }
}