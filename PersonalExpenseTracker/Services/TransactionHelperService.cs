using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using PersonalExpenseTracker.Dtos;
using PersonalExpenseTracker.Models;

namespace PersonalExpenseTracker.Services
{
    public class TransactionHelperService : ITransactionHelperService
    {
        public readonly IDebtService DebtService;

        public TransactionHelperService(IDebtService debtService)
        {
            DebtService = debtService;
        }

        public List<Transactions> ExtractInflowOutflowTransactions(List<Transactions> transactions)
        {
            List<Transactions> inflowOutflowTransactions = transactions.Where(t => t.TypeId != "Debt").ToList();
            return inflowOutflowTransactions;
        }

        public List<Transactions> ExtractDebtTransactions(List<Transactions> transactions)
        {
            List<Transactions> debtTransactions = transactions.Where(t => t.TypeId == "Debt").ToList();
            return debtTransactions;
        }

        public async Task<List<DebtTransactionDto>> LoadDebtTransactions(List<Transactions> debtTransactions)
        {
            List<DebtTransactionDto> debtTransactionDtos = new List<DebtTransactionDto>();

            foreach (Transactions debtTransaction in debtTransactions)
            {
                Debts debt = await DebtService.LoadUsersTransactionsDebtsAsync(debtTransaction.TransactionId);

                DebtTransactionDto debtTransactionDto = new DebtTransactionDto(debtTransaction.TransactionId, debtTransaction.TypeId, debtTransaction.UserId,
                    debtTransaction.TagId, debtTransaction.Amount, debtTransaction.Date,
                    debtTransaction.Note, debtTransaction.Title, debt.DebtId, debt.DueDate, debt.Source, debt.Status);

                debtTransactionDtos.Add(debtTransactionDto);
            }

            return debtTransactionDtos;
        }

        public List<DebtTransactionDto> ExtractRemainingDebts(List<DebtTransactionDto> debtTransactionDtos)
        {
            List<DebtTransactionDto> remainingDebts = debtTransactionDtos.Where(t => t.Status == "Pending").ToList();
            return remainingDebts;
        }

        public List<DebtTransactionDto> ExtractClearedDebts(List<DebtTransactionDto> debtTransactionDtos)
        {
            List<DebtTransactionDto> clearedDebts = debtTransactionDtos.Where(t => t.Status == "Cleared").ToList();
            return clearedDebts;
        }


        public double CalculateTotalInflow(List<Transactions> inflowTransactions)
        {
            double totalInflow = inflowTransactions.Where(t => t.TypeId == "Credit").Sum(t => t.Amount);
            return totalInflow;
        }

        public double CalculateTotalOutflow(List<Transactions> outflowTransactions)
        {
            double totalOutflow = outflowTransactions.Where(t => t.TypeId == "Debit").Sum(t => t.Amount);
            return totalOutflow;
        }

        public double CalculateTotalRemainingDebt(List<DebtTransactionDto> debtTransactionDtos)
        {
            double totalRemainingDebt = debtTransactionDtos.Where(d => d.Status == "Pending").Sum(d => d.Amount);
            return totalRemainingDebt;
        }

        public double CalculateTotalClearedDebt(List<DebtTransactionDto> debtTransactionDtos)
        {
            double totalClearedDebt = debtTransactionDtos.Where(d => d.Status == "Cleared").Sum(d => d.Amount);
            return totalClearedDebt;
        }

        public double CalculateCurrentBalance(double totalInflow, double totalOutflow, double totalPendingDebt)
        {
            double currentBalance = totalInflow - totalOutflow + totalPendingDebt;
            return currentBalance;
        }

    }
}
