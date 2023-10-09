﻿using TestDomain;

namespace Domain
{
    public class Exchange
    {
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
                    throw new Exception();
                }
                else
                {
                    _dollarValue = value;
                }
            }
        }

        public Workspace Workspace { get; set; }
    }
}