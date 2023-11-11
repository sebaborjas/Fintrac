using Domain;
using Domain.Exceptions;
using System.Xml.Linq;

namespace Domain
{
    public class Workspace
    {
        public int ID { get; set; }

        private string _name;
        private string _userAdminId;
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

        public string UserAdminId { get { return _userAdminId; } set { _userAdminId = UserAdmin.Email; } }
        public List<Account> AccountList { get; } = new List<Account>();
        public List<Report> ReportList { get; } = new List<Report>();
        public List<Exchange> ExchangeList { get; } = new List<Exchange>();
        public List<Category> CategoryList { get; } = new List<Category>();
        public List<Goal> GoalList { get; } = new List<Goal>();

        public override bool Equals(object? obj)
        {
            Workspace workspace = (Workspace)obj;
            return this.ID == workspace.ID;
        }
    }
}