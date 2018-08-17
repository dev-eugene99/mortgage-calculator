using MortgageCalculator.Services;
using System;
using System.Text;


namespace MortgageCalculator
{
    class Program
    {
        private static readonly MortgageService service = new MortgageService();

        private const string ErrorMsg = 
            @"Invalid input, please provide a properly formatted file.  
              The following information should be provided:
              amount: 
              interest:
              downpayment:
              term:";

        //filename assumed to be the first argument
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ReportError();
                return;
            }

            string fileName = args[0];
            var inputSB = new StringBuilder();
            string[] lines = System.IO.File.ReadAllLines(fileName);

            foreach (var line in lines)
            {
                inputSB.Append($"{line.Trim().ToLower()} ");
            }

            if (inputSB.Length == 0)
            {
                ReportError();
                return;
            }


            try
            {
                Console.WriteLine(service.CalculateMortgage(inputSB.ToString().Trim()));
                return;
            }
            catch (Exception)
            {
                ReportError();
            }
        }

        static void ReportError()
        {
            Console.WriteLine(ErrorMsg);
        }
    }
}
