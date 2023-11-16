using Domain.DataTypes;
using Domain.Exceptions;

namespace Domain
{
    public class Transaction
    {

        public int ID { get; set; }
        private string _title;
        private DateTime _creationDate = DateTime.Today;
        private double _amount;
        private Category _category;
        private Account _account;

        public int AccountId {  get; private set; }
        public int CategoryId { get; private set; }

        public string Title
        {
            get => _title;
            set
            {
                if (value == "")
                {
                    throw new EmptyFieldException();
                }
                else
                {
                    _title = value;
                }
            }
        }
        public DateTime CreationDate
        {
            get => _creationDate;
            set
            {
                if (value > DateTime.Today)
                {
                    throw new ArgumentException("La fecha es invalida");
                }
                else
                {
                    _creationDate = value;
                }
            }
        }
        public double Amount
        {
            get => _amount;
            set
            {
                if (value < 0)
                {
                    throw new Exception();
                }
                else
                {
                    _amount = value;
                }
            }
        }
        public CurrencyType Currency { get; set; }

        public Category Category
        {
            get => _category;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("No se permiten categorias vacias");
                }
                if (value.Status == CategoryStatus.Inactive)
                {
                    throw new InactiveCategoryException();
                }
                _category = value;
                CategoryId = value.Id;
            }
        }
        public Account Account
        {
            get
            {
                return _account;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("No se permiten cuentas vacias");
                }
                if (value.Currency != Currency)
                {
                    throw new InvalidTransactionCurrencyException();
                }
                _account = value;
                AccountId = value.Id;
            }
        }

        public override bool Equals(object? obj)
        {
            Transaction transaction = (Transaction)obj;
            return this.ID == transaction.ID;
        }
    }
}