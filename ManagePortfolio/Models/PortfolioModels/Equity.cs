using System;
using System.Collections.Generic;
using System.Linq;

namespace ManagePortfolio.Models.PortfolioModels
{
    public interface IEquity
    {
        /// <summary>
        /// RIC of the stock
        /// </summary>
        string Code { get; }

        /// <summary>
        /// Name of the stock
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Total shares of all active stock position
        /// </summary>
        int Shares { get; }

        /// <summary>
        /// Current price of the stock
        /// </summary>
        double Price { get; }

        /// <summary>
        /// Average price of all active stock position
        /// </summary>
        double AveragePrice { get; }

        /// <summary>
        /// Total value of all active stock position
        /// </summary>
        double TotalValue { get; }

        /// <summary>
        /// Book profit of all active stock position
        /// </summary>
        double BookProfit { get; }

        /// <summary>
        /// Realized profit of all completed stock position
        /// </summary>
        double RealizedProfit { get; }

        /// <summary>
        /// All transaction records of the stock
        /// </summary>
        IList<IEquityTransaction> Transactions { get; }

        /// <summary>
        /// Add active position to transaction records
        /// </summary>
        void CreateTransaction(int shares, double priceBuy, DateTime dateBuy);

        /// <summary>
        /// Add completed position to transaction records
        /// </summary>
        void CreateTransaction(int shares, double priceBuy, double priceSell, DateTime dateBuy, DateTime dateSell);
    }

    public class Equity : IEquity
    {
        public Equity(string code, string name)
            : this(code, name, new List<IEquityTransaction>())
        {
        }

        public Equity(string code, string name, List<IEquityTransaction> transactions)
        {
            Code = code;
            Name = name;
            Transactions = transactions;
        }

        #region Properties

        public string Code { get; }
        public string Name { get; }
        public double Price { get; set; }

        public double TotalValue
        {
            get
            {
                return Transactions.Where(trans => trans.State != TransactionState.Completed)
                                   .Sum(trans => trans.Shares * Price);
            }
        }

        public double BookProfit
        {
            get
            {
                return Transactions.Where(trans => trans.State != TransactionState.Completed)
                                   .Sum(trans => (Price - trans.PriceBuy) * trans.Shares);
            }
        }

        public double RealizedProfit
        {
            get
            {
                return Transactions.Where(trans => trans.State == TransactionState.Completed)
                                   .Sum(trans => (trans.PriceSell - trans.PriceBuy) * trans.Shares);
            }
        }

        public double AveragePrice
        {
            get
            {
                return Transactions.Where(trans => trans.State != TransactionState.Completed)
                                   .Average(trans => trans.PriceBuy);
            }
        }

        public int Shares
        {
            get
            {
                return Transactions.Where(trans => trans.State != TransactionState.Completed)
                                   .Select(trans => trans.Shares)
                                   .Aggregate((s1, s2) => s1 + s2);
            }
        }

        public IList<IEquityTransaction> Transactions { get; }

        #endregion

        #region Functions

        /// <summary>
        /// Create Active Transaction
        /// </summary>
        public void CreateTransaction(int shares, double priceBuy, DateTime dateBuy)
        {
            Transactions.Add(new EquityTransaction(shares, priceBuy, dateBuy));
        }

        /// <summary>
        /// Create Completed Transaction
        /// </summary>
        public void CreateTransaction(int shares, double priceBuy, double priceSell, DateTime dateBuy, DateTime dateSell)
        {
            Transactions.Add(new EquityTransaction(shares, priceBuy, priceSell, dateBuy, dateSell));
        }

        #endregion
    }
}