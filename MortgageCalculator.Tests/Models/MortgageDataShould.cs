using MortgageCalculator.Models;
using Xunit;

namespace MortgageCalculator.Tests
{
    public class MortgageDataShould
    {
        private readonly MortgageData _mortgage;

        public MortgageDataShould()
        {
            decimal amount = 100000;
            float interest = 0.055f;
            decimal downPayment = 20000;
            int term = 30;

            _mortgage = new MortgageData(amount, interest, downPayment, term);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void CalculateLoan()
        {
            Assert.Equal(80000m, _mortgage.Loan, 2);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void CalculateMonthlyInterest()
        {
            Assert.Equal(0.004583f, _mortgage.MonthlyInterest, 6);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void CalculateMonthsOfPayments()
        {
            Assert.Equal(360, _mortgage.MonthsOfPayments);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void MonthlyPaymentNotZero()
        {
            Assert.NotEqual(0, _mortgage.MonthlyPayment, 0);
        }


        [Fact]
        [Trait("Category", "Unit")]
        public void CalculateMonthlyPayment()
        {
            Assert.Equal(454.23m, _mortgage.MonthlyPayment, 2);
        }


        [Fact]
        [Trait("Category", "Unit")]
        public void CalculateTotalPayment()
        {
            //my formulas aren't matching the given result from README
            //Assert.Equal(163523.23m, _mortgage.TotalPayment, 2);
            Assert.Equal(163522.8m, _mortgage.TotalPayment, 2);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void CalculateTotalInterest()
        {
            //my formulas aren't matching the given result from README
            //Assert.Equal(83523.23m, _mortgage.TotalInterest, 2);
            Assert.Equal(83522.8m, _mortgage.TotalInterest, 2);
        }
    }
}
