namespace Domain
{
    public class CategoryGoal
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int GoalId { get; set; }
        public Goal Goal { get; set; }
    }
}

