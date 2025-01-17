using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalExpenseTracker.Dtos;
using PersonalExpenseTracker.Models;

namespace PersonalExpenseTracker.Services
{
    public interface ITransactionHelperService
    {

        List<Transactions> ExtractInflowOutflowTransactions(List<Transactions> transactions);

        List<Transactions> ExtractDebtTransactions(List<Transactions> transactions);

        Task<List<DebtTransactionDto>> LoadDebtTransactions(List<Transactions> debtTransactions);

        List<DebtTransactionDto> ExtractRemainingDebts(List<DebtTransactionDto> debtTransactionDtos);

        List<DebtTransactionDto> ExtractClearedDebts(List<DebtTransactionDto> debtTransactionDtos);

        double CalculateTotalInflow(List<Transactions> inflowTransactions);

        double CalculateTotalOutflow(List<Transactions> outflowTransactions);

        double CalculateTotalRemainingDebt(List<DebtTransactionDto> debtTransactionDtos);

        double CalculateTotalClearedDebt(List<DebtTransactionDto> debtTransactionDtos);

        double CalculateCurrentBalance(double totalInflow, double totalOutflow, double totalPendingDebt);
    }
}
