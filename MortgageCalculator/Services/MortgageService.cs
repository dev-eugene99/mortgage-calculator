using MortgageCalculator.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MortgageCalculator.Services
{
    public class MortgageService
    {
        public string ProcessMortgageFile(string fileName)
        {
            StringBuilder inputSB = ReadFileIntoStringBuilder(fileName);
            return CalculateMortgage(inputSB.ToString().Trim());
        }

        public StringBuilder ReadFileIntoStringBuilder(string fileName)
        {
            var inputSB = new StringBuilder();
            string[] lines = System.IO.File.ReadAllLines(fileName);

            foreach (var line in lines)
                inputSB.Append($"{line.Trim().ToLower()} ");

            if (inputSB.Length == 0)
                throw new ArgumentException();

            return inputSB;
        }

        public string CalculateMortgage(string unformattedInput)
        {
            MortgageData data = ExtractMortgageData(unformattedInput);
            MortgageSummaryDTO summary = PrepareDataForOutput(data);
            return summary.toJSONString();
        }

        public MortgageData ExtractMortgageData(string unformattedInput)
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

        public MortgageSummaryDTO PrepareDataForOutput(MortgageData data)
        {            

            var summary = new MortgageSummaryDTO(
                data.MonthlyPayment,
                data.TotalInterest,
                data.TotalPayment
                );
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
