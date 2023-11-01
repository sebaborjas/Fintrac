using Domain;

namespace BusinessLogic
{
    public class MemoryDatabase
    {
        public bool isLoggedIn { get; set; } = false;
        public List<User> Users { get; set; } = new List<User>();
        public User currentUser { get; set; }
        public Account currentAccount { get; set; }
        public Workspace currentWorkspace { get; set; }
        public MemoryDatabase()
        {
            Users = new List<User>();
        }

    }

}