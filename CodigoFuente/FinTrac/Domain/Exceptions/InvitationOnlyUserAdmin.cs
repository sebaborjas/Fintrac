using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class InvitationOnlyUserAdmin : Exception
    {
        public static string Message = "Solo el usuario administrador del espacio puede enviar invitaciones";

        public InvitationOnlyUserAdmin() : this(Message)
        {

        }

        private InvitationOnlyUserAdmin(string? message) : base(message)
        {

        }
    }
}