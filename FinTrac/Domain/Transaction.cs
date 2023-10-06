using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Domain.Excepciones;

namespace Domain
{
	public class Transaction{

		private string _title;
		private DateTime _creationDate;
		private double _amount;
		private string _currency;
		private Category _category;
		private string _account;

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
			set => _creationDate = value;
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

		public string Currency
		{
			get => _currency;
			set => _currency = value;
		}

		public Category Category
		{
			get => _category;
			set => _category = value;
		}
		public string Account
		{
			get => _account;
			set => _account = value;
		}
		
		public Transaction()
		{
		}

	}
}