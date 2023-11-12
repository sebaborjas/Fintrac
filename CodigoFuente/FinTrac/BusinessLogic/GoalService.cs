using Domain;
using Domain.DataTypes;
using Domain.Exceptions;

namespace BusinessLogic
{
    public class GoalService
    {
        private readonly MemoryDatabase _memoryDatabase;
        private Random random = new Random();

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
                    oldGoal.Token = newGoal.Token;
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
                    oldGoal.Token = newGoal.Token;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public void GenerateUniqueToken(Goal goal)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            goal.Token = new string(Enumerable.Repeat(chars, 12)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}