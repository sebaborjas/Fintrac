using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Domain.DataTypes;
using Domain.Exceptions;

namespace Domain
{
	public class Category
	{
		private string _name;
		private DateTime _creationDate;
		private CategoryType _type;
		private CategoryStatus _status;
		
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				if (value == "")
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
				if (value < DateTime.Today)
				{
					throw new ArgumentException();
				}
				if (value > DateTime.Today)
				{
					throw new ArgumentException();
				}
				_creationDate = value;

			}
		}
		public CategoryType Type
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
			}
		}
		public CategoryStatus Status
		{
			get
			{
				return _status;
			}
			set
			{
				_status = value;
			}
		}

	}
}
