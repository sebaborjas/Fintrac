using Domain;
using Domain.Exceptions;
using System.Xml.Linq;

namespace TestDomain
{
    public class Workspace
    {
        public static int Id { get; private set; } = 1;

        public Workspace(User userAdmin, string name)
        {
            Id++;
            Name = name;
        }

        private string _name;
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
                else
                {
                    _name = value;
                }
            }
        }
        public User UserAdmin { get; set; }
        public List<object> AccountList { get; } = new List<object>();
        public List<object> ReportList { get; } = new List<object> { };
        public List<object> ExchangeList { get; } = new List<object> { };
        public List<object> CategoryList { get; } = new List<object>();
        public List<object> GoalList { get; } = new List<object>();
    }
}