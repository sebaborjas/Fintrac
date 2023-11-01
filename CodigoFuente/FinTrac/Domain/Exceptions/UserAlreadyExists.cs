using System.Runtime.Serialization;

namespace Domain.Exceptions
{
	[Serializable]
	public class UserAlreadyExistsException : Exception
	{
		public static string Message = "El email ya está en uso";

		public UserAlreadyExistsException() : this(Message)
		{

		}

		private UserAlreadyExistsException(string? message) : base(message)
		{

		}

	}
}