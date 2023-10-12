using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain
{

    public class CreditCard : Account
    {
        private string _bankName;
        private string _lastDigits;
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

        public string LastDigits
        {
            get
            {
                return _lastDigits;
            }
            set
            {
                string pattern = @"^\d{4}$";
                Regex regex = new(pattern, RegexOptions.IgnoreCase);

                if (!regex.IsMatch(value))
                {
                    throw new ArgumentException("Ingrese los últimos 4 dígitos");
                }
                if (value.Length != 4)
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

        public override void Update(Account account)
        {
            CreditCard creditCard = account as CreditCard;
            string pattern = @"^\d{4}$";
            Regex regex = new(pattern, RegexOptions.IgnoreCase);

            if (!regex.IsMatch(creditCard.LastDigits))
            {
                throw new ArgumentException("Ingrese los últimos 4 dígitos");
            }
            if (creditCard.LastDigits.Length != 4)
            {
                throw new ArgumentException("Ingrese los últimos 4 números");
            }
            this.LastDigits = creditCard.LastDigits;
            this.Name = creditCard.Name;
            this.CreationDate = creditCard.CreationDate;
            this.BankName = creditCard.BankName;
            this.LastDigits = creditCard.LastDigits;
            this.AvailableCredit = creditCard.AvailableCredit;
            this.DeadLine = creditCard.DeadLine;
        }

    }
}
