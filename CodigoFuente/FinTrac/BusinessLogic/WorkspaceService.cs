using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Exceptions;
using Domain.DataTypes;

namespace BusinessLogic
{
    public class WorkspaceService
    {
        private readonly FintracContext _database;

        public WorkspaceService(FintracContext database)
        {
            this._database = database;
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
                _database.SaveChanges();
            }
            catch (Exception exception)
            {
                throw exception;
            }         
        }

        public Workspace Get(int ID)
        {

            return _database.Users.First(x => x.WorkspaceList.Any(x => x.ID == ID)).WorkspaceList.First(x => x.ID == ID);

        }

        public void UpdateName(Workspace workspace, string newName)
        {

            _database.Users.ToList().FindAll(x => x.WorkspaceList.Contains(workspace)).ForEach(x => x.WorkspaceList.Find(x => x.ID == workspace.ID).Name = newName);
            workspace.Name = newName;

            _database.SaveChanges();
        }

        public void DeleteWorkspace(Workspace workspace)
        {
            User oldUserAdmin = workspace.UserAdmin;

            List<User> users = _database.Users.ToList();

            int workspaceUsersCount = 0;

            foreach(User user in users)
            {
                foreach(Workspace currentWorkspace in user.WorkspaceList)
                {
                    if(currentWorkspace.ID == workspace.ID)
                    {
                        workspaceUsersCount++;
                    }
                }
            }

            

            if (workspaceUsersCount > 1)
            {
                User newUserAdmin = _database.Users.First(x => x.WorkspaceList.Contains(workspace) && x != oldUserAdmin);
                workspace.UserAdmin = newUserAdmin;
            }
            else
            {
                oldUserAdmin.WorkspaceList.Remove(workspace);
                _database.Users.First(x => x == oldUserAdmin).WorkspaceList.Remove(workspace);
                _database.SaveChanges();
            }
            
        }

        		public List<Transactions> ListAllTransactionsAllAcounts(Workspace workspace)
		{
			List<Transactions> transactionList = new List<Transactions>();
			foreach (Account account in workspace.AccountList)
			{
				transactionList.AddRange(account.TransactionList);
			}
			return transactionList;
		}
		public List<User> ListGuestUsers(Workspace workspace)
		{

			List<User> guestUsersWorkspace = _database.Users.Where(u => u.WorkspaceList.Contains(workspace)).ToList();
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
