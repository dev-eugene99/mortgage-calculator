﻿using MortgageCalculator.Models;
using MortgageCalculator.Services;
using System;
using Xunit;

namespace MortgageCalculator.Tests
{
    public class MortgageServiceShould
    {
        private readonly MortgageService _mortgageService;

        public MortgageServiceShould()
        {
            _mortgageService = new MortgageService();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ExtractMortgageDataFromValidString()
        {            
            var input = @"amount: 10000 interest: 0.035 downpayment: 1000 term: 15";
            var expected = new MortgageData(10000m, 0.035f, 1000m, 15);

            var data = _mortgageService.ExtractMortgageData(input);

            Assert.IsType<MortgageData>(data);
            Assert.Equal(expected.Amount, data.Amount);
            Assert.Equal(expected.Interest, data.Interest);
            Assert.Equal(expected.DownPayment, data.DownPayment);
            Assert.Equal(expected.Term, data.Term);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ExtractMortgageDataFromValidStringWithLessSpaces()
        {
            var input = @"amount:10000 interest:0.035 downpayment:1000 term:15";
            var expected = new MortgageData(10000m, 0.035f, 1000m, 15);

            var data = _mortgageService.ExtractMortgageData(input);

            Assert.Equal(expected.Amount, data.Amount);
            Assert.Equal(expected.Interest, data.Interest);
            Assert.Equal(expected.DownPayment, data.DownPayment);
            Assert.Equal(expected.Term, data.Term);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ExtractInterestRatesFromPercentage()
        {
            var input = @"amount: 10000 interest: 3.5% downpayment: 1000 term: 15";

            var data = _mortgageService.ExtractMortgageData(input);

            Assert.Equal(0.035f, data.Interest);
        }


        [Fact]
        [Trait("Category", "Unit")]
        public void ExtractAmountFromCurrencyString()
        {
            var input = @"amount: $10,000 interest: 0.035 downpayment: 1000 term: 15";
            var expected = new MortgageData(10000m, 0.035f, 1000m, 15);

            var data = _mortgageService.ExtractMortgageData(input);

            Assert.Equal(10000m, data.Amount);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ThrowExceptionFromInValidString()
        {
            var input = @"amount: 10000 interest: 0.035 ddownpayment: 1000 term: 15";

            Assert.Throws<ArgumentException>(() => _mortgageService.ExtractMortgageData(input));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void CreateStringBuilderFromValidFile()
        {
            var fileName = "loan1.txt";
            var expected = "amount : 100000 interest:5.5% downpayment: $20000 term:30 ";

            var outSB = _mortgageService.ReadFileIntoStringBuilder(fileName);

            Assert.Equal(expected, outSB.ToString());
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void GenerateMortgageSummaryDTO()
        {
            var input = new MortgageData(10000m, 0.035f, 1000m, 15);
            var expected = new MortgageSummaryDTO(input.MonthlyPayment, input.TotalInterest, input.TotalPayment);

            var output = _mortgageService.PrepareDataForOutput(input);

            Assert.IsType<MortgageSummaryDTO>(output);
            Assert.Equal(expected.MonthlyPayment, output.MonthlyPayment);
            Assert.Equal(expected.TotalInterest, output.TotalInterest);
            Assert.Equal(expected.TotalPayment, output.TotalPayment);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void GenerateJSONMortgageFileFromValidFile()
        {
            var fileName = "loan1.txt";
            var expected = "{ \"monthly payment\":454.23, \"total interest\":83522.8, \"total payment\":163522.8 }";
            var output = _mortgageService.ProcessMortgageFile(fileName);

            Assert.Equal(expected, output);
        }


    }
}
