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

		public override void Update(Account account)
		{
			
			PersonalAccount personalAccount = account as PersonalAccount;

			this.Name = personalAccount.Name;
			this.CreationDate = personalAccount.CreationDate;
			this.WorkSpace = personalAccount.WorkSpace;
			this.Currency = personalAccount.Currency;
			this.TransactionList = personalAccount.TransactionList;
			this.StartingAmount = personalAccount.StartingAmount;
		}


    }
}
