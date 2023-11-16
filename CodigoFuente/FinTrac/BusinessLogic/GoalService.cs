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
		private Random random = new Random();
		public GoalService(FintracContext database)
		{
			_database = database;
		}
		public void Add(Workspace workspace, Goal goal)
		{
			if (workspace.Goals.Contains(goal))
			{
				throw new GoalAlreadyExistsException();
			}
			if (goal.Categories.Count == 0)
			{
				throw new EmptyCategoryListException();
			}
			try
			{
				workspace.Goals.Add(goal);
			}
			catch (Exception exception)
			{
				throw exception;
			}

			_database.SaveChanges();
		}
		public Goal Get(Workspace workspace, string title)
		{
			return _database.Users.Where(x => x.Workspaces.Contains(workspace)).FirstOrDefault<User>().Workspaces.Find(x => x.ID == workspace.ID).Goals.Find(x => x.Title == title);
		}
		public void Delete(Workspace workspace, Goal goal)
		{
			try
			{
				if (workspace.Goals.Contains(goal))
				{
					workspace.Goals.Remove(goal);

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
					oldGoal.Categories = new List<Category>(newGoal.Categories);
					oldGoal.Token = newGoal.Token;
				}
				else
				{
					if (oldGoal.Workspace.Goals.Contains(newGoal))
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
			_database.SaveChanges();
		}

		public void GenerateUniqueToken(Goal goal)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			goal.Token = new string(Enumerable.Repeat(chars, 12)
			  .Select(s => s[random.Next(s.Length)]).ToArray());
		}
	}
}