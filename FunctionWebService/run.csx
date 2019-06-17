#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Net.Http;

public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];


            var client = new HttpClient();
       
            var response = await client.GetAsync("http://dummy.restapiexample.com/api/v1/employee/52448");
            var content = await response.Content.ReadAsStringAsync();

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return content != null
                ? (ActionResult)new OkObjectResult($"contect:, {content}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
}
