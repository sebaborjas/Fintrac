using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
	public class CategoryReport : Report
	{
		public Category Category { get; set; }

		public double AmountSpent { get; set; } = 0;

		public string PercentageAboutTotal { get; set; } = "0";

		public string CalculateReport() 
		{
			return "";
		}

	}
}
