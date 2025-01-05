using PersonalExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;

namespace PersonalExpenseTracker.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly string transactionsFilePath = Path.Combine(AppContext.BaseDirectory, "Transactions.json");
       
        public async Task<List<Transactions>> LoadUsersTransactionsAsync(int userId)
        {
            try
            {
                var transactions = await GetAllTransactionsAsync();
                // Return transactions filtered by userId or an empty list if tasks is null
                return (transactions ?? new List<Transactions>()).Where(t => t.UserId == userId).ToList();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error retrieving tasks for user {userId}: {ex.Message}");
                return new List<Transactions>(); // Return an empty list in case of an exception
            }
        }

        public async Task<int> SaveTransactionAsync(Transactions transaction)
        {
            try
            {
                var transactions = await GetAllTransactionsAsync();

                //setting transaction id
                int transactionsCount = transactions.Count();
                transaction.TransactionId = transactionsCount + 1;

                //setting transaction creation date
                transaction.Date = DateTime.Now.Date;

                transactions.Add(transaction);
                await WriteTransactionsToJson(transactions);

                //If the transaction is of type debt, the extra debt information also needs to be saved
                //The debt record has a transaction id to identigy which transaction the debt record is created for.
                //So the transaction id is returned, so it can be used by the debt record
                return transaction.TransactionId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving transaction: {ex.Message}");
                throw;
            }
        }

        private async Task<List<Transactions>> GetAllTransactionsAsync()
        {
            try
            {
                if (!File.Exists(transactionsFilePath))
                {
                    return new List<Transactions>();
                }

                var json = await File.ReadAllTextAsync(transactionsFilePath);
                return JsonSerializer.Deserialize<List<Transactions>>(json) ?? new List<Transactions>();
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"JSON deserialization error: {jsonEx.Message}");
                return new List<Transactions>();
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"I/O error while loading transactions: {ioEx.Message}");
                return new List<Transactions>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while loading transactions: {ex.Message}");
                return new List<Transactions>();
            }
        }

        private async Task WriteTransactionsToJson(List<Transactions> transactions)
        {
            try
            {
                var json = JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true });

                await File.WriteAllTextAsync(transactionsFilePath, json);
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"I/O error while loading transactions: {ioEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while saving transactions: {ex.Message}");
            }
        }
    }
}
