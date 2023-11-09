using Domain;

namespace Domain
{
    public class Exchange
    {
        public int Id { get; set; }
        private DateTime _date;
        private double _dollarValue;

        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                if (value > DateTime.Today)
                {
                    throw new ArgumentException("La fecha es invalida");
                }
                else
                {
                    _date = value;
                }
            }
        }

        public double DollarValue {
            get
            {
                return _dollarValue;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("El valor del dolar debe ser mayor a 0");
                }
                else
                {
                    _dollarValue = value;
                }
            }
        }

        public Workspace Workspace { get; set; }

        public override bool Equals(object? obj)
        {
            Exchange exchange = (Exchange)obj;
            return this.Date == exchange.Date;
        }

    }
}