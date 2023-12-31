﻿using Domain.DataTypes;

namespace Domain
{
    public class Exchange
    {
        public int Id { get; set; }
        private DateTime _date;
        private double _currencyValue;

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

        public double CurrencyValue {
            get
            {
                return _currencyValue;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("El valor de la moneda debe ser mayor a 0");
                }
                else
                {
                    _currencyValue = value;
                }
            }
        }
        public CurrencyType Currency { get; set;}
        public Workspace Workspace { get; set; }

        public override bool Equals(object? obj)
        {
            Exchange exchange = (Exchange)obj;
            return this.Date == exchange.Date && this.Currency == exchange.Currency;
        }
    }
}