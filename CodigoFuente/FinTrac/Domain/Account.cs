using System.ComponentModel;
using Domain.DataTypes;
using Domain.Exceptions;

namespace Domain
{
    public abstract class Account
    {
        public int Id { get; set; }
        private string _name;
        private DateTime _creationDate = DateTime.Today;
        private Workspace _workSpace;
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

        public Workspace WorkSpace 
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

        public List<Transaction> TransactionList { get; set; } = new List<Transaction>();

        public abstract void Update(Account account);

        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }
            if(obj.GetType() != this.GetType())
            {
                return false;
            }
            Account account = (Account)obj;
            return account.Name == this.Name;
        }       

    }
}