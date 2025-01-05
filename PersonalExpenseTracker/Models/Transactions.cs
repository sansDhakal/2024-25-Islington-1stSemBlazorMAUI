using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalExpenseTracker.Models
{
    public class Transactions

    {
        public int TransactionId { get; set; }
        public string TypeId { get; set; }

        public int UserId { get; set; }
        public string TagId { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Note { get; set; }

        public string Title { get; set; }
       
        
    }
}
