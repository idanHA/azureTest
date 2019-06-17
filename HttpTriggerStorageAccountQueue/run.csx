#r "Newtonsoft.Json"
#r "Microsoft.WindowsAzure.Storage"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

public static async Task<IActionResult> Run(HttpRequest req, ILogger log,ICollector<string> outputQueueItem)
{
    log.LogInformation("C# HTTP trigger function processed a request.");

    string name = req.Query["name"];

    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    dynamic data = JsonConvert.DeserializeObject(requestBody);
    name = name ?? data?.name;

    var message = $"Message-From-{name}-HTTP-Trigger-{DateTime.Now.Minute}";
 
   try
   {
        //log.LogInformation($"Sending message: {message}");
        outputQueueItem.Add(name);
        log.LogInformation($"Sending message: {outputQueueItem}");
   }
   catch (StorageException se)
   {
      log.LogInformation($"StorageException: {se.Message}");
   }
   catch (Exception ex)
   {
      log.LogInformation($"{DateTime.Now} > Exception: {ex.Message}");
   }


    return message != null
        ? (ActionResult)new OkObjectResult($"This message, {message} was sent to the Message Queue")
        : new BadRequestObjectResult("Something bad happened, check the logs...");
}
