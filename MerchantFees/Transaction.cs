using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantFees
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public Merchant Merchant { get; set; }
        public double Amount { get; set; }

        public Transaction()
        {

        }

        public static string GetMonthlyFeesForMerchant(string month, string merchantName, List<Transaction> transactions)
        {
            return $"{month} {merchantName} {transactions.Count} {String.Format("{0:0.00}", CalculateFees(transactions))}";
        }

        public static double CalculateFees(List<Transaction> transactions)
        {
            var total = transactions.Sum(transaction => transaction.Amount);
            var fees = total * 0.01;
            var discount = transactions.All(transaction => transaction.Merchant.Type == MerchantType.BIG) ? fees * 0.1 : 0;
            return Math.Round(fees - discount, 2);
        }

        public static Dictionary<string, Dictionary<string, List<Transaction>>> GetGroupedTransactions()
        {
            var transactions = TransactionsFileReader.GetTransactions();
            return transactions
                .GroupBy(t => t.Date.Year + "-" + t.Date.Month)
                .OrderByDescending(d => d.Key)
                .ToDictionary(g => g.Key, g => GetMerchantGrouping(g));
        }

        private static Dictionary<string, List<Transaction>> GetMerchantGrouping(IGrouping<string, Transaction> grouping)
        {
            return grouping.GroupBy(t => t.Merchant.Name)
                    .OrderBy(d => d.Key)
                    .ToDictionary(g2 => g2.Key, g2 => g2.ToList());
        }
    }
}
