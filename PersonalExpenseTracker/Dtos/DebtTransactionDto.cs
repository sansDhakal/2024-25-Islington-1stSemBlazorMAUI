using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalExpenseTracker.Dtos
{
    public class DebtTransactionDto
    {
        public int TransactionId { get; set; }
        public string TypeId { get; set; }

        public int UserId { get; set; }
        public string TagId { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Note { get; set; }

        public string Title { get; set; }

        public int DebtId { get; set; }

        public string Status { get; set; }

        public DateTime DueDate { get; set; }
        public string Source { get; set; }

        public DebtTransactionDto(int transactionId, string typeId, int userId, string tagId, double amount,
            DateTime date, string? note, string title, int debtId, DateTime dueDate, string source, string status)
        {
            TransactionId = transactionId;
            TypeId = typeId;
            UserId = userId;
            TagId = tagId;
            Amount = amount;
            Date = date;
            Note = note;
            Title = title;
            DebtId = debtId;
            DueDate = dueDate;
            Source = source;
            Status = status;
        }
    }
}
