using MortgageCalculator.Core.Models;
using Xunit;

namespace MortgageCalculator.Tests
{
    public class MortgageSummaryDTOShould
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void CreateJSONString()
        {
            var expected = "{ \"monthly payment\":5.05, \"total interest\":120.25, \"total payment\":350.75 }";

            var summary = new MortgageSummaryDTO(5.05m, 120.25m, 350.75m);

            Assert.Equal(expected, summary.toJSONString());
        }

    }
}