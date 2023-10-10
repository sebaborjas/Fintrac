using Domain;
using Domain.Exceptions;
using System.Xml.Linq;

namespace Domain
{
    public class Workspace
    {
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
        public List<Account> AccountList { get; } = new List<Account>();
        public List<Report> ReportList { get; } = new List<Report> { };
        public List<Exchange> ExchangeList { get; } = new List<Exchange> { };
        public List<Category> CategoryList { get; } = new List<Category>();
        public List<Goal> GoalList { get; } = new List<Goal>();
    }
}