using PersonalExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;

namespace PersonalExpenseTracker.Services
{
    public class DebtService : IDebtService
    {
        private readonly string debtsFilePath = Path.Combine(AppContext.BaseDirectory, "Debts.json");

        public async Task<Debts> LoadUsersTransactionsDebtsAsync(int transactionId)
        {
            try
            {
                var debts = await GetAllDebtsAsync();

                // Return transactions filtered by userId or an empty list if tasks is null
                return (debts ?? new List<Debts>()).FirstOrDefault(d => d.TransactionId == transactionId);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error retrieving tasks for transaction {transactionId}: {ex.Message}");
                return new Debts(); // Return an empty list in case of an exception
            }
        }

        public async Task SaveDebtAsync(Debts debt)
        {
            try
            {
                var debts = await GetAllDebtsAsync();

                //setting debt id
                int existingDebtsCount = debts.Count();
                debt.DebtId = existingDebtsCount + 1;

                debts.Add(debt);
                await WriteDebtsToJson(debts);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving transaction: {ex.Message}");
                throw;
            }
        }

        private async Task<List<Debts>> GetAllDebtsAsync()
        {
            try
            {
                if (!File.Exists(debtsFilePath))
                {
                    return new List<Debts>();
                }

                var json = await File.ReadAllTextAsync(debtsFilePath);
                return JsonSerializer.Deserialize<List<Debts>>(json) ?? new List<Debts>();
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"JSON deserialization error: {jsonEx.Message}");
                return new List<Debts>();
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"I/O error while loading debts: {ioEx.Message}");
                return new List<Debts>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while loading debts: {ex.Message}");
                return new List<Debts>();
            }
        }

        private async Task WriteDebtsToJson(List<Debts> debts)
        {
            try
            {
                var json = JsonSerializer.Serialize(debts, new JsonSerializerOptions { WriteIndented = true });

                await File.WriteAllTextAsync(debtsFilePath, json);
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"I/O error while loading debts: {ioEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while saving debts: {ex.Message}");
            }
        }
    }
}
