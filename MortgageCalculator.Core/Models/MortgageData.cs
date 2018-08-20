using System;

namespace MortgageCalculator.Core.Models
{
    public class MortgageData
    {
        public decimal Amount { get; private set; }
        public float Interest { get; private set; }
        public decimal DownPayment { get; private set; }
        public int Term { get; private set; }

        public decimal Loan => (Amount - DownPayment);
        public float MonthlyInterest => Interest / 12;
        public int MonthsOfPayments => Term * 12;

        private decimal _monthlyPayment;
        public decimal MonthlyPayment
        {
            get
            {
                if (_monthlyPayment == 0)
                {
                    if (MonthlyInterest == 0)
                    {
                        _monthlyPayment = Loan / MonthsOfPayments;
                    }
                    else
                    {
                        // P = L[c(1 + c)^n] / [(1 + c)^n - 1]
                        _monthlyPayment = Math.Round(
                            ((Loan * (decimal)(MonthlyInterest * Math.Pow(1 + MonthlyInterest, MonthsOfPayments)))
                            / (decimal)(Math.Pow(1 + MonthlyInterest, MonthsOfPayments) - 1)), 2
                              );
                    }
                }
                return _monthlyPayment;
            }
        }

        public decimal TotalPayment => MonthlyPayment * MonthsOfPayments;
        public decimal TotalInterest => TotalPayment - Loan;

        public MortgageData(decimal amount, float interest, decimal downPayment, int term)
        {
            Amount = amount;
            Interest = interest;
            DownPayment = downPayment;
            Term = term;
            _monthlyPayment = 0;
        }
    }
}
