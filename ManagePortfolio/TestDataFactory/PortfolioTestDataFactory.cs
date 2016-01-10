using System;
using System.Collections.Generic;
using ManagePortfolio.Models.PortfolioModels;
using WebGrease.Css.Extensions;

namespace ManagePortfolio.TestDataFactory
{
    public class PortfolioTestDataFactory
    {
        private static readonly Random Random = new Random(DateTime.Now.Millisecond);

        private static readonly IDictionary<string, TestDataStructure> TestCases = new Dictionary
            <string, TestDataStructure>
        {
            {
                "0005.HK", new TestDataStructure
                {
                    Name = "HSBC",
                    Lot = 400,
                    MaxPrice = 100,
                    MinPrice = 50,
                    Transactions = 10,
                    ActiveTransactions = 4,
                    MaxTransactionPrice = 150,
                    MinTransactionPrice = 20
                }
            },
            {
                "0017.HK", new TestDataStructure
                {
                    Name = "New World Development Co.",
                    Lot = 1000,
                    MaxPrice = 20,
                    MinPrice = 6,
                    Transactions = 5,
                    ActiveTransactions = 2,
                    MaxTransactionPrice = 10,
                    MinTransactionPrice = 7
                }
            }
        };

        public static IList<IEquity> GenerateTestEquities()
        {
            var equities = new List<IEquity>();

            TestCases.ForEach(tc =>
            {
                var testCase = tc.Value;
                var activeCount = testCase.ActiveTransactions;
                var transactions = new List<IEquityTransaction>();

                // Create Transactions
                for (var i = 0; i < testCase.Transactions; i++)
                {
                    var shares = testCase.Lot*Random.Next(1, 10);
                    var priceBuy = (Random.NextDouble()*(testCase.MaxTransactionPrice - testCase.MinTransactionPrice)) +
                                   testCase.MinTransactionPrice;
                    var dateBuy = DateTime.Now;

                    if (i < activeCount)
                    {
                        // Active transaction
                        transactions.Add(new EquityTransaction(shares, priceBuy, dateBuy));
                    }
                    else
                    {
                        // Completed transaction
                        var priceSell = (Random.NextDouble()*
                                         (testCase.MaxTransactionPrice - testCase.MinTransactionPrice)) +
                                        testCase.MinTransactionPrice;
                        var dateSell = DateTime.Now;
                        dateBuy = dateSell.AddDays(Random.NextDouble()*-10);

                        transactions.Add(new EquityTransaction(shares, priceBuy, priceSell, dateBuy, dateSell));
                    }
                }

                // Create Equity
                equities.Add(new Equity(tc.Key, testCase.Name, transactions)
                {
                    Price = (Random.NextDouble()*(testCase.MaxPrice - testCase.MinPrice)) + testCase.MinPrice
                });
            });

            return equities;
        }

        private class TestDataStructure
        {
            public string Name { get; set; }
            public int MaxPrice { get; set; }
            public int MinPrice { get; set; }
            public int Transactions { get; set; }
            public int ActiveTransactions { get; set; }
            public int MaxTransactionPrice { get; set; }
            public int MinTransactionPrice { get; set; }
            public int Lot { get; set; }
        }
    }
}