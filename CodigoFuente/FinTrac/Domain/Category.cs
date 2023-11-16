using Domain.DataTypes;
using Domain.Exceptions;

namespace Domain
{
	public class Category
	{
		public int Id { get; set; }
		private string _name;
		private DateTime _creationDate = DateTime.Today;
		private CategoryType _type;
		private CategoryStatus _status;
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				if (value == "")
				{
					throw new EmptyFieldException();
				}
				_name = value;

			}
		}
		public DateTime CreationDate
		{
			get
			{
				return _creationDate;
			}
			set
			{
				if (value > DateTime.Today)
				{
					throw new ArgumentException();
				}
				_creationDate = value;

			}
		}
		public CategoryType Type
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
			}
		}
		public CategoryStatus Status
		{
			get
			{
				return _status;
			}
			set
			{
				_status = value;
			}
		}
		public Workspace Workspace { get; set; }
		public List<Goal> Goals { get; set; }
		public List<CategoryGoal> GoalCategory { get; set; }

		public override bool Equals(object? obj)
		{
			Category category = (Category)obj;
			return this.Name == category.Name;
		}
	}
}
