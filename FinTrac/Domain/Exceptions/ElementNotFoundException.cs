using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException(string message) : base(message)
        {

        }

    }
}