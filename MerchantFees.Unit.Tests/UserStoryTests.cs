using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MerchantFees.Unit.Tests
{
    [TestClass]
    public class UserStoryTests
    {
        [TestMethod]
        [DataRow(100.00, 50.00, 1.5)]
        [DataRow(125.00, 1.00, 1.26)]
        [DataRow(44.10, 11.33, 0.5543)]
        [DataRow(10.10, 11.11, 0.2121)]
        [DataRow(505050.50, 0.33, 5050.5083)]
        public void RegularMerchantIsChargedOnePercentOfTotalTransaction(double amount1, double amount2, double expectedFee)
        {
            //Arrange
            var transactions = new List<Transaction>()
            {
                new Transaction() {
                    Date = DateTime.Now.AddDays(-3),
                    Merchant = new Merchant() { Name = "TestMerchant" },
                    Amount = amount1
                },
                new Transaction() {
                    Date = DateTime.Now.AddDays(-3),
                    Merchant = new Merchant() { Name = "TestMerchant" },
                    Amount = amount2
                }
            };

            //Act
            var result = Transaction.CalculateFees(transactions);

            //Assert
            Assert.AreEqual(expectedFee, result, $"Fee should be ({amount1} + {amount2}) * 0.01 = {expectedFee}");
        }

        [TestMethod]
        [DataRow(100.00, 50.00, 1.35)]
        [DataRow(125.00, 1.00, 1.134)]
        [DataRow(10.10, 11.11, 0.19089)]
        [DataRow(505050.50, 0.33, 4545.45747)]
        public void BigMerchantGetsTenPercentDiscountOnFees(double amount1, double amount2, double expectedFee)
        {
            //Arrange
            var transactions = new List<Transaction>()
            {
                new Transaction() {
                    Date = DateTime.Now.AddDays(-3),
                    Merchant = new Merchant() { Name = "TELIA" },
                    Amount = amount1
                },
                new Transaction() {
                    Date = DateTime.Now.AddDays(-3),
                    Merchant = new Merchant() { Name = "TELIA" },
                    Amount = amount2
                }
            };

            //Act
            var result = Transaction.CalculateFees(transactions);

            //Assert
            Assert.AreEqual(expectedFee, result, $"Fee should be {expectedFee}");
        }

        [TestMethod]
        public void TransactionsFileIsReadCorrectly()
        {
            //Arrange
            
            //Act
            var result = TransactionsFileReader.GetTransactions();

            //Assert
            Assert.IsTrue(result.Count > 0, $"List<Transaction> should contain items");
        }

        [TestMethod]
        public void GroupingProducesDictionaryWithResults()
        {
            //Arrange

            //Act
            var result = Transaction.GetGroupedTransactions();

            //Assert
            Assert.IsTrue(result.Count > 0, $"Dictionary<sring, List<Transaction>> should contain items");
        }
    }
}
