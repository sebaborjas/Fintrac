using Domain.DataTypes;

namespace Domain
{
    public abstract class Account
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public Currency Currency { get; set; }

        /*public WorkSpace workSpace { get; set; }*/

    }
}