using Domain;
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
	}

}