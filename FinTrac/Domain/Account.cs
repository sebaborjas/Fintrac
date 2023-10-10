using System.ComponentModel;
using Domain.DataTypes;
using Domain.Exceptions;

namespace Domain
{
    public abstract class Account
    {
        private string _name;
        private DateTime _creationDate = DateTime.Today;
        private Object _workSpace;
        public string Name 
        {
            get
            {
                return _name;
            }
            set
            {
                if(value == "")
                {
                    throw new EmptyFieldException();
                }
                _name = value;
            }
        }
        public DateTime CreationDate {
            get
            {
                return _creationDate;
            }
            set
            {
                if(value > DateTime.Today)
                {
                    throw new ArgumentException("La fecha es invalida");
                }
                _creationDate = value;
            } 
        }
        public CurrencyType Currency { get; set; } = CurrencyType.UYU;

        public Object WorkSpace 
        {
            get
            {
                return _workSpace;
            }
            set
            {
                if(value == null)
                {
                    throw new ArgumentNullException("No se permiten espacios de trabajo vacios");
                }
                _workSpace = value;
            } 
        }

    }
}