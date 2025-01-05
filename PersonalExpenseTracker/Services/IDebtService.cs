using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalExpenseTracker.Models;

namespace PersonalExpenseTracker.Services
{
    public interface IDebtService
    {
        Task SaveDebtAsync(Debts debt);

        Task<Debts> LoadUsersTransactionsDebtsAsync(int transactionId);
    }
}
