# Mortgage Calculator

A simple console app that takes in a file with the following format:
```
amount: 100000
interest: 5.5%
downpayment: 20000
term: 30

```
NOTE: the last line of input is a blank line.
The term is given in years.  The interest can be given a percentage or a digit.
The downpayment or the amount can be represented in US money standard, i.e. $125,000.00
The 4 fields can be in any order.

it will return a JSON string as output in the console, such as:
```
{
    "monthly payment": 454.23,
    "total interest": 83523.23
    "total payment" 163523.23
}
```

## How to run:

1. Install [.NET Core 2.1](https://www.microsoft.com/net/download/dotnet-core/2.1) or later:
2. open command line or powershell and navigate into `MortgageCalculator` project.
3. This program takes a file name as a parameter.  
4. example: `dotnet run loan1.txt`.

## How to test:

1. open command line or powershell and navigate into `MortgageCalculator.Tests` project.
2. execute `dotnet test` to run all tests.
3. for only unit tests, execute `dotnet test --filter Category=unit`
3. for only integration tests, execute `dotnet test --filter Category=integration`

## Future potential features:

- Process multiple mortgage calculations in one batch.
- Allow user to specify file name to write the json output into.
