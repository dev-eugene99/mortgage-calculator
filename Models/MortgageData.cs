using System;

namespace MortgageCalculator.Models
{
    public class MortgageData
    {
        public decimal Amount { get; set; }
        public float Interest { get; set; }
        public decimal DownPayment { get; set; }
        public int Term { get; set; }

        public decimal Loan => (Amount - DownPayment);
        public float MonthlyInterest => Interest / 12;
        public int MonthsOfPayments => Term * 12;

        // P = L[c(1 + c)^n] / [(1 + c)^n - 1]
        public decimal MonthlyPayment =>
            Math.Round(
                    ((Loan * (decimal)(MonthlyInterest * Math.Pow(1 + MonthlyInterest, MonthsOfPayments))) 
                    / (decimal)(Math.Pow(1 + MonthlyInterest, MonthsOfPayments) - 1)), 2
                      );

        public decimal TotalPayment => MonthlyPayment * MonthsOfPayments;
        public decimal TotalInterest => TotalPayment - Loan;
    }
}
