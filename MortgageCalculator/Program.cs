using MortgageCalculator.Services;
using System;


namespace MortgageCalculator
{
    public class Program
    {
        private static readonly MortgageService _mortgageService = new MortgageService();

        private const string ErrorMsg = 
            @"Invalid input, please provide a properly formatted file.  
              The following information should be provided:
              amount: 
              interest:
              downpayment:
              term:";

        //filename assumed to be the first argument
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ReportError();
            }
            else
            {
                try
                {
                    var JSON = _mortgageService.ProcessMortgageFile(args[0]);
                    Console.WriteLine(JSON);
                }
                catch (Exception)
                {
                    ReportError();
                }
            }
        }

        static void ReportError()
        {
            Console.WriteLine(ErrorMsg);
        }
    }
}
