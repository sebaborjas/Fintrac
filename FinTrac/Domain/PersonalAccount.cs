using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
	public class PersonalAccount : Account
	{
		private double _startingAmount;
		public double StartingAmount 
		{
			get
			{
				return _startingAmount;
			}
			set
			{
				if(value < 0)
				{
					throw new ArgumentException("El monto inicial ingresado es inválido");
				}
				_startingAmount = value;
			}
		}
	}
}
