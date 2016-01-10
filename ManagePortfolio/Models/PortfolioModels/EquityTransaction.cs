using System;

namespace ManagePortfolio.Models.PortfolioModels
{
    public enum TransactionState
    {
        Active,
        Completed
    }

    public interface IEquityTransaction
    {
        int Shares { get; }
        double PriceBuy { get; }
        double PriceSell { get; }
        DateTime DateBuy { get; }
        DateTime DateSell { get; }
        TransactionState State { get; }

        /// <summary>
        /// Realized profit of all completed stock position
        /// </summary>
        double RealizedProfit { get; }
    }

    public class EquityTransaction : IEquityTransaction
    {
        public int Shares { get; private set; }
        public double PriceBuy { get; private set; }
        public double PriceSell { get; private set; }
        public DateTime DateBuy { get; private set; }
        public DateTime DateSell { get; private set; }
        public TransactionState State { get; private set; }

        public double RealizedProfit => State == TransactionState.Completed ? (PriceSell - PriceBuy) * Shares 
                                                                            : 0;

        public EquityTransaction(int shares, double priceBuy, DateTime dateBuy)
            : this(shares, priceBuy, default(double), dateBuy, default(DateTime), TransactionState.Active)
        {
        }

        public EquityTransaction(int shares, double priceBuy, double priceSell, DateTime dateBuy, DateTime dateSell)
            : this(shares, priceBuy, priceSell, dateBuy, dateSell, TransactionState.Completed)
        {
        }

        private EquityTransaction(int shares, double priceBuy, double priceSell, DateTime dateBuy, DateTime dateSell, TransactionState state)
        {
            Shares = shares;
            PriceBuy = priceBuy;
            PriceSell = priceSell;
            DateBuy = dateBuy;
            DateSell = dateSell;
            State = state;
        }
    }
}