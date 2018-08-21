using Newtonsoft.Json;

namespace MortgageCalculator.Core.Models
{
    public class MortgageSummaryDTO
    {
        [JsonProperty(PropertyName = "monthly payment")]
        public decimal MonthlyPayment { get; set; }
        [JsonProperty(PropertyName = "total interest")]
        public decimal TotalInterest { get; set; }
        [JsonProperty(PropertyName = "total payment")]
        public decimal TotalPayment { get; set; }


        public MortgageSummaryDTO(decimal monthlyPayment, decimal totalInterest, decimal totalPayment)
        {
            MonthlyPayment = monthlyPayment;
            TotalInterest = totalInterest;
            TotalPayment = totalPayment;
        }

        public string toJSONString()
        {
            return JsonConvert.SerializeObject(this).ToLower();
        }
    }
}
