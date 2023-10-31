using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
	public class GoalsReport : Report
	{
		public double DefinedAmount { get; set; }

		public double AmountSpent { get; set; }

		public bool GoalAchieved { get; set; }
	}
}
