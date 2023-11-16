
namespace Domain
{
	public class PersonalAccount : Account
	{
        public int Id { get; set; }

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
			this.Transactions = personalAccount.Transactions;
			this.StartingAmount = personalAccount.StartingAmount;
		}
    }
}
