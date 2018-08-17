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
2. open command line or powershell and navigate to the folder this project resides in.
3. This program takes a file name as a parameter.  
4. example: `dotnet run loan1.txt`.
