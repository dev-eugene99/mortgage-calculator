namespace MortgageCalculator.Core.Models
{
    public class MortgageSummaryDTO
    {
        public decimal MonthlyPayment { get; set; }
        public decimal TotalPayment { get; set; }
        public decimal TotalInterest { get; set; }

        public MortgageSummaryDTO(decimal monthlyPayment, decimal totalInterest, decimal totalPayment)
        {
            MonthlyPayment = monthlyPayment;
            TotalInterest = totalInterest;
            TotalPayment = totalPayment;
        }

        public string toJSONString()
        {
            return $"{{ \"monthly payment\":{MonthlyPayment:0.##}, \"total interest\":{TotalInterest:0.##}, \"total payment\":{TotalPayment:0.##} }}";
        }
    }
}
