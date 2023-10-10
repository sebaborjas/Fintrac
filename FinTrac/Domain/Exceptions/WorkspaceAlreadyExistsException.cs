using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class WorkspaceAlreadyExistsException : Exception
    {
        public static string Message = "El email ya está en uso";

        public WorkspaceAlreadyExistsException() : this(Message)
        {

        }

        private WorkspaceAlreadyExistsException(string? message) : base(message)
        {

        }

    }
}