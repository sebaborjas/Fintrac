using Domain.Exceptions;

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
        public List<Account> Accounts { get; } = new List<Account>();

        public List<User> Users { get; } = new List<User>();

        public List<Exchange> Exchanges { get; } = new List<Exchange>();
        public List<Category> Categories { get; } = new List<Category>();
        public List<Goal> Goals { get; } = new List<Goal>();
        public List<UserWorkspace> UserWorkspace { get; set; }


        public override bool Equals(object? obj)
        {
            Workspace workspace = (Workspace)obj;
            return this.ID == workspace.ID;
        }
    }
}