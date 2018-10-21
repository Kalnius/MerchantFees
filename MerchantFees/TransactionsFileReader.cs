using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantFees
{
    public class TransactionsFileReader
    {
        public static List<Transaction> GetTransactions()
        {
            var lines = File.ReadLines("transactions.txt").ToList();
            var result = lines.ConvertAll(line => ParseTransaction(line));
            return result;
        }

        private static Transaction ParseTransaction(string line)
        {
            var split = line.Split(' ');
            return new Transaction()
            {
                Date = DateTime.Parse(split[0]),
                Merchant = new Merchant() {
                    Name = split[1]
                },
                Amount = Double.Parse(split[2])
            };
        }
    }
}
