using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ExpensesReport
    {
        public Workspace Workspace { get; set; }

        public List<Transaction> ListExpenses() {
            throw new NotImplementedException();
        }

        public List<Transaction> ListExpensesByCategory(string categoryName)
        {
            throw new NotImplementedException();
        }

        public List<Transaction> ListExpensesByDate(DateTime fromDate, DateTime toDate)
        {
            throw new NotImplementedException();
        }

        public List<Transaction> ListExpensesByAccount(string accountName)
        {
            throw new NotImplementedException();
        }
    }
}
