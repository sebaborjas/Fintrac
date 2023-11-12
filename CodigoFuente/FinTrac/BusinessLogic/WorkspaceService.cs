using Domain;
using Domain.DataTypes;
using Domain.Exceptions;

namespace BusinessLogic
{
	public class WorkspaceService
	{
		private readonly MemoryDatabase _memoryDatabase;

		public WorkspaceService(MemoryDatabase memoryDatabase)
		{
			this._memoryDatabase = memoryDatabase;
		}


		public void Add(User user, Workspace w)
		{
			if (user.WorkspaceList.Contains(w))
			{
				throw new WorkspaceAlreadyExistsException();
			}
			try
			{

				user.WorkspaceList.Add(w);

			}
			catch (Exception exception)
			{
				throw exception;
			}
		}

		public Workspace Get(int ID)
		{

			return _memoryDatabase.Users.First(x => x.WorkspaceList.Any(x => x.ID == ID)).WorkspaceList.First(x => x.ID == ID);

		}

		public void UpdateName(Workspace workspace, string newName)
		{

			_memoryDatabase.Users.FindAll(x => x.WorkspaceList.Contains(workspace)).ForEach(x => x.WorkspaceList.Find(x => x.ID == workspace.ID).Name = newName);
			workspace.Name = newName;

		}

		public void DeleteWorkspace(Workspace workspace)
		{
			User oldUserAdmin = workspace.UserAdmin;
			if (_memoryDatabase.Users.Count(x => x.WorkspaceList.Contains(workspace)) > 1)
			{
				User newUserAdmin = _memoryDatabase.Users.First(x => x.WorkspaceList.Contains(workspace) && x != oldUserAdmin);
				workspace.UserAdmin = newUserAdmin;
			}
			else
			{
				oldUserAdmin.WorkspaceList.Remove(workspace);
				_memoryDatabase.Users.First(x => x == oldUserAdmin).WorkspaceList.Remove(workspace);
			}
		}

		public List<Transaction> ListAllTransactionsAllAcounts(Workspace workspace)
		{
			List<Transaction> transactionList = new List<Transaction>();
			foreach (Account account in workspace.AccountList)
			{
				transactionList.AddRange(account.TransactionList);
			}
			return transactionList;
		}
		public List<User> ListGuestUsers(Workspace workspace)
		{

			List<User> guestUsersWorkspace = _memoryDatabase.Users.Where(u => u.WorkspaceList.Contains(workspace)).ToList();
			return guestUsersWorkspace;
		}
		public List<CreditCard> GetCreditCards(Workspace workspace)
		{
			List<CreditCard> creditCards = new List<CreditCard>();

			foreach (Account account in workspace.AccountList)
			{
				if (account is CreditCard creditCard)
				{
					creditCards.Add(creditCard);
				}
			}

			return creditCards;
		}

		public List<PersonalAccount> GetPersonalAccounts(Workspace workspace)
		{
			List<PersonalAccount> personalAccounts = new List<PersonalAccount>();

			foreach (Account account in workspace.AccountList)
			{
				if (account is PersonalAccount personalAccount)
				{
					personalAccounts.Add(personalAccount);
				}
			}

			return personalAccounts;
		}

		public List<GoalsReport> GenerateGoalsReport(Workspace workspace)
		{
			List<GoalsReport> goalsReports = new List<GoalsReport>();

			foreach (Goal goal in workspace.GoalList)
			{
				GoalsReport report = new GoalsReport
				{
					WorkSpace = workspace,
					Goal = goal
				};
				report.CalculateReport();

				goalsReports.Add(report);
			}

			return goalsReports;
		}

		public List<CategoryReport> GenerateCategoryReports(Workspace workspace, Month month)
		{
			List<CategoryReport> categoryReports = new List<CategoryReport>();

			foreach (var category in workspace.CategoryList)
			{
				CategoryReport report = new CategoryReport
				{
					WorkSpace = workspace,
					Month = month,
					Category = category
				};

				report.CalculateReport();
				categoryReports.Add(report);
			}

			return categoryReports;
		}

		public List<DailyReport> GenerateMonthlyReport(Workspace workspace, Month month, int year)
		{
			List<DailyReport> DailyReports = new List<DailyReport>();

			for (int i = 1; i <= DateTime.DaysInMonth(year, (int)month); i++)
			{
				DailyReport dailyReport = new DailyReport
				{
					WorkSpace = workspace,
					Date = new DateTime(year, (int)month, i)
				};
				dailyReport.CalculateReport();
				DailyReports.Add(dailyReport);
			}
			return DailyReports;
		}
	}

}
