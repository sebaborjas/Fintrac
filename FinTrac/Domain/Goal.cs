using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Domain
{
	public class Goal
	{
		private string _title;
		private double _maxAmount;
		private List<Category> categories;
		private Object _workspace;

		public string Title
		{
			get
			{
				return _title;
			}
			
			set
			{
				if (value == "")
				{
					throw new EmptyFieldException();
				}
				_title = value;
			}
		}

		public double MaxAmount
		{
			get
			{
				return _maxAmount;
			}
			
			set
			{
				if (value <= 0)
				{
					throw new ArgumentException();
				}
				_maxAmount = value;
			}
		}	

		public List<Category> Categories
		{
			get
			{
				return categories;
			}
			
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				categories = value;
			}
		}	

		public Object Workspace
		{
			get
			{
				return _workspace;
			}
			
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				_workspace = value;
			}
		}	
	}
}
