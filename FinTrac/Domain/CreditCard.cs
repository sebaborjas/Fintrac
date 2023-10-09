using Domain.Excepciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain
{

    public class CreditCard : Account
    {
        private string _bankName;
        private int _lastDigits;
        private double _availableCredit;
        private int _deadLine;
        public string BankName
        {
            get
            {
                return _bankName;
            }
            set
            {
                if (value == "")
                {
                    throw new EmptyFieldException();
                }
                _bankName = value;
            }
        }

        public int LastDigits
        {
            get
            {
                return _lastDigits;
            }
            set
            {
                if (value.ToString().Length != 4)
                {
                    throw new ArgumentException("Ingrese los últimos 4 números");
                }
                _lastDigits = value;
            }
        }

        public double AvailableCredit
        {
            get
            {
                return _availableCredit;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Ingrese un número mayor a cero");
                }
                _availableCredit = value;
            }
        }

        public int DeadLine
        {
            get
            {
                return _deadLine;
            }
            set
            {
                if(value < 1 || value > 28)
                {
                    throw new ArgumentException("Ingrese un número entre 1 y 28");
                }
                _deadLine = value;  
            }
        }

    }
}
