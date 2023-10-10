using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class WorkspaceAlreadyExistsException : Exception
    {
        public static string Message = "El Workspace ya existe";

        public WorkspaceAlreadyExistsException() : this(Message)
        {

        }

        private WorkspaceAlreadyExistsException(string? message) : base(message)
        {

        }

    }
}