using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MortgageCalculator.Core.Services;
using Newtonsoft.Json;
using System.IO;

namespace MortgageCalculator.Func
{
    public static class MortgageCalculator
    {
        private static readonly MortgageService _mortgageService = new MortgageService();
        [FunctionName("CalculateMortgage")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            decimal amount = data?.amount ?? 0;
            float interest = data?.interest ?? 0;
            decimal downpayment = data?.downpayment ?? 0; ;
            int term = data?.downpayment ?? 0;

            var mortgageData = _mortgageService.CalculateMortgageFromData(amount, interest, downpayment, term);

            return mortgageData != null 
                ? (ActionResult)new OkObjectResult(mortgageData.toJSONString())
                : new BadRequestObjectResult("");
        }
    }
}
