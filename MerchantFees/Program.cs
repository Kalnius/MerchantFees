using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantFees
{
    class Program
    {
        static void Main(string[] args)
        {
            var transactions = Transaction.GetGroupedTransactions();
            foreach (var month in transactions)
            {
                var builder = new StringBuilder();
                foreach (var merchant in month.Value)
                {
                    builder.AppendLine(Transaction.GetMonthlyFeesForMerchant(month.Key, merchant.Key, merchant.Value));
                }
                Console.Write(builder.ToString());
            }
            Console.ReadKey();
        }
    }
}