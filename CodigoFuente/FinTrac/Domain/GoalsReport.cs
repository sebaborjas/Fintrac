namespace Domain
{
	public class GoalsReport : Report
	{
		private double _amountSpent;

		public bool GoalAchieved { get; set; }

		public double AmountSpent
		{
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

		public Goal Goal { get; set; }

		public void CalculateReport()
		{
			List<Account> accounts = this.WorkSpace.Accounts;
			foreach (Category category in Goal.Categories)
			{
				foreach (Account account in accounts)
				{
					List<Transactions> transactions = account.Transactions;
					foreach (Transactions transaction in transactions)
					{
						if (transaction.Category == category && transaction.CreationDate.Month == DateTime.Today.Month)
						{
							if (account.Currency == DataTypes.CurrencyType.UYU)
							{
								AmountSpent += transaction.Amount;
							}
							else
							{
								double currencyValue = this.GetExchangeValueOfDay(transaction.CreationDate, transaction.Currency);
								double transactionAmountInUYU = transaction.Amount * currencyValue;
								AmountSpent += transactionAmountInUYU;
							}
						}
					}
				}
			}
			GoalAchieved = AmountSpent <= Goal.MaxAmount;
		}
	}
}