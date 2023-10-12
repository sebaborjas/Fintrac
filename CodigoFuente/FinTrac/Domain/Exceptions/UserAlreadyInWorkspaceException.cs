using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class UserAlreadyInWorkspaceException : Exception
    {
        public static string Message = "El usuario ya se encuentra en el espacio";

        public UserAlreadyInWorkspaceException() : this(Message)
        {

        }

        private UserAlreadyInWorkspaceException(string? message) : base(message)
        {

        }

    }
}