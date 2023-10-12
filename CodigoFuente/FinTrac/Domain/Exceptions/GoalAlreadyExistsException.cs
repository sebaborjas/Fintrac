using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class GoalAlreadyExistsException : Exception
    {
        public static string Message = "Ya se existe un objetivo con ese nombre";

        public GoalAlreadyExistsException() : this(Message)
        {

        }

        private GoalAlreadyExistsException(string? message) : base(message)
        {

        }

    }
}