using MortgageCalculator.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MortgageCalculator.Services
{
    public class MortgageService
    {
        public string CalculateMortgage(string unformattedInput)
        {
            var data = extractMortgageData(unformattedInput);
            var summary = ComputeMortgage(data);
            return summary.toJSONString();
        }

        public MortgageData extractMortgageData(string unformattedInput)
        {
            var words = unformattedInput.Split(' ', ':');
            List<string> wordsList = new List<string>();
            foreach(var w in words)
            {
                if (!string.IsNullOrEmpty(w))
                    wordsList.Add(w);
            }

            decimal amount = 0;
            float interest = 0;
            decimal downPayment = 0;
            int term = 0;

            for (int i = 0; i < wordsList.Count; i = i + 2)
            {
                var keyword = wordsList[i];
                switch (keyword)
                {
                    case "amount":
                        amount = ParseDecimal(wordsList[i + 1]);
                        break;
                    case "interest":
                        interest = ParsePercentage(wordsList[i + 1]);
                        break;
                    case "downpayment":
                        downPayment = ParseDecimal(wordsList[i + 1]);
                        break;
                    case "term":
                        term = int.Parse(wordsList[i + 1]);
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
            
            return new MortgageData(amount, interest, downPayment, term);
        }

        public MortgageSummaryDTO ComputeMortgage(MortgageData data)
        {            

            var summary = new MortgageSummaryDTO(
                data.MonthlyPayment,
                data.TotalPayment,
                data.TotalInterest
                );
            //fill in the blank
            return summary;
        }

        private float ParsePercentage(string valueString)
        {
            if (valueString.EndsWith("%"))
            {
                valueString = valueString.Substring(0, valueString.Length - 1);
                return float.Parse(valueString) / 100;
            }

            return float.Parse(valueString);
        }

        private static decimal ParseDecimal(string valueString)
        {
            if (valueString.Contains("$") || valueString.Contains(","))
            {
                return decimal.Parse(valueString, NumberStyles.Currency);
            }
            else
            {
                return decimal.Parse(valueString);
            }
        }
    }
}
