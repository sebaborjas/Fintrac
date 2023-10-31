using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
	public class GoalsReport : Report
	{
		private double _definedAmount;

		private double _amountSpent;

		public bool GoalAchieved { get; set; }

		public double DefinedAmount {
			get 
			{
				return _definedAmount;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("El monto definido no puede ser negativo");
				}
				_definedAmount = value;
			}
		}

		public double AmountSpent {
			get
			{
				return _amountSpent;
			}
			set
			{
				if (value < 0) 
				{
					throw new ArgumentException("El monto definido no puede ser negativo");
				}
				_amountSpent = value;
			}
		}

		public void CalculateReport()
		{
			GoalAchieved = AmountSpent <= DefinedAmount;
		}
	}
}
