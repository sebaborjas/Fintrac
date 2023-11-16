using Domain.DataTypes;

namespace Domain
{
	public class AccountBalanceReport : Report
	{
		public int Id { get; set; }
		private PersonalAccount _account;
		public PersonalAccount Account
		{
			get
			{
				return this._account;
			}
			set
			{
				if (value == null)
				{
					throw new NullReferenceException("No se admite una cuenta vacia");
				}
				this._account = value;
			}
		}
		public double CalculateBalance()
		{
			double ingresos = 0;
			double costos = 0;
			double balance = 0;

			foreach (var transaction in Account.Transactions)
			{
				if (transaction.Currency == CurrencyType.UYU)
				{
					if (transaction.Category.Type == CategoryType.Income)
					{
						ingresos += transaction.Amount;
					}
					else
					{
						costos += transaction.Amount;
					}

					balance = this.Account.StartingAmount + ingresos - costos;
				}
				else
				{
					if (transaction.Category.Type == CategoryType.Income)
					{
						ingresos += transaction.Amount * base.GetExchangeValueOfDay(transaction.CreationDate, transaction.Currency);
					}
					else
					{
						costos += transaction.Amount * base.GetExchangeValueOfDay(transaction.CreationDate, transaction.Currency);
					}
					balance = this.Account.StartingAmount * base.GetExchangeValueOfDay(Account.CreationDate, Account.Currency) + ingresos - costos;
				}
			}
			return balance;
		}
	}
}
