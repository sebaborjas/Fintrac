using Domain;
using Domain.DataTypes;
using Domain.Exceptions;

namespace BusinessLogic
{
	public class GoalService
	{
		private readonly MemoryDatabase _memoryDatabase;

		public GoalService(MemoryDatabase memoryDatabase)
		{
			_memoryDatabase = memoryDatabase;
		}

		public void Add(Workspace workspace, Goal goal)
		{
			if (workspace.GoalList.Contains(goal))
			{
				throw new GoalAlreadyExistsException();
			}
			if (goal.Categories.Count == 0)
			{
				throw new EmptyCategoryListException();
			}
			try
			{
				workspace.GoalList.Add(goal);
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}

		public Goal Get(Workspace workspace, string title)
		{
			return _memoryDatabase.Users.Find(x => x.WorkspaceList.Contains(workspace)).WorkspaceList.Find(x => x.ID == workspace.ID).GoalList.Find(x => x.Title == title);
		}

		public void Delete(Workspace workspace, Goal goal)
		{
			try
			{
				if (workspace.GoalList.Contains(goal))
				{
					workspace.GoalList.Remove(goal);

				}
			}
			catch (Exception exception)
			{
				throw exception;
			}

		}

		public void AddCategoryToGoal(Goal goal, List<Category> categories)
		{
			foreach (var category in categories)
			{
				if (category.Type == CategoryType.Cost && category.Status == CategoryStatus.Active)
				{
					goal.Categories.Add(category);
				}
			}
		}

		public void Update(Goal oldGoal, Goal newGoal)
		{
			try
			{
				if (oldGoal.Title == newGoal.Title)
				{
					oldGoal.Title = newGoal.Title;
					oldGoal.MaxAmount = newGoal.MaxAmount;
					oldGoal.Categories = newGoal.Categories;
					oldGoal.Categories = new List<Category>(newGoal.Categories);
				}
				else
				{
					if (oldGoal.Workspace.GoalList.Contains(newGoal))
					{
						throw new GoalAlreadyExistsException();
					}
					oldGoal.Title = newGoal.Title;
					oldGoal.MaxAmount = newGoal.MaxAmount;
					oldGoal.Categories = newGoal.Categories;
					oldGoal.Categories = new List<Category>(newGoal.Categories);
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}

	}
}