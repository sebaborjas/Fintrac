using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Domain;
using Domain.DataTypes;
using Domain.Exceptions;

namespace BusinessLogic
{
    public class GoalService
    {
        private readonly FintracContext _database;

        public GoalService(FintracContext database)
        {
            _database = database;
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

            _database.SaveChanges();
        }

        public Goal Get(Workspace workspace, string title)
        {
            return _database.Users.Where(x => x.WorkspaceList.Contains(workspace)).FirstOrDefault<User>().WorkspaceList.Find(x => x.ID == workspace.ID).GoalList.Find(x => x.Title == title);
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

            _database.SaveChanges();
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

            _database.SaveChanges();
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
                }
                else
                {
                    if (newGoal.Workspace.GoalList.Contains(newGoal))
                    {
                        throw new GoalAlreadyExistsException();
                    }
                    oldGoal.Title = newGoal.Title;
                    oldGoal.MaxAmount = newGoal.MaxAmount;
                    oldGoal.Categories = newGoal.Categories;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            _database.SaveChanges();
        }

    }
}