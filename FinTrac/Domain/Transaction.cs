using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Domain.DataTypes;
using Domain.Excepciones;

namespace Domain
{
	public class Transaction{

		private string _title;
		private DateTime _creationDate = DateTime.Today;
		private double _amount;
		private CurrencyType _currency;
		private Category _category;
		private Object _account;
		private Object _workSpace;

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

		public CurrencyType Currency
		{
			get => _currency;
			set
			{
				if (value == CurrencyType.UYU || value == CurrencyType.USD)
				{
					_currency = value;
				}
				else
				{
					throw new Exception();
				}
				
			}
		}

		public Category Category
		{
			get => _category;
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("No se permiten categorias vacias");
				}
				_category = value;
			}	

		}

		public Object Account
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
				_account = value;
			}
		}

		public Object WorkSpace
		{
			get
			{
				return _workSpace;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("No se permiten espacios de trabajo vacios");
				}
				_workSpace = value;
			}
		}
	}
}