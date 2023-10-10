using System.Runtime.Serialization;

namespace BusinessLogic
{
    [Serializable]
    internal class WorkspaceHasUsersException : Exception
    {
        public WorkspaceHasUsersException()
        {
        }

        public WorkspaceHasUsersException(string? message) : base(message)
        {
        }

        public WorkspaceHasUsersException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected WorkspaceHasUsersException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}