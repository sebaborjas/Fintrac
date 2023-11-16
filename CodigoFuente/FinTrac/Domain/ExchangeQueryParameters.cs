using Domain.DataTypes;

namespace Domain
{
    public class ExchangeQueryParameters
    {
        public Workspace Workspace { get; set; }
        public DateTime Date { get; set; }
        public CurrencyType CurrencyType { get; set; }
    }
}
