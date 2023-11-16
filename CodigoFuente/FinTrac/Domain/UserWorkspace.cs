namespace Domain
{
    public class UserWorkspace
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int WorkspaceId { get; set; }
        public Workspace Workspace { get; set; }
    }
}

