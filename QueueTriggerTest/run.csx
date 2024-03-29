using System;

public static void Run(string myQueueItem,ICollector<Message> outputTable, ILogger log)
{
    log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");


            outputTable.Add(
                new Message() { 
                        PartitionKey = Guid.NewGuid().ToString("n"), 
                        RowKey =Math.Floor((DateTime.UtcNow - new DateTime(1970,1,1)).TotalMilliseconds).ToString(),
                        Content = myQueueItem
                     }

                );
}

public class Message
{
    public string PartitionKey {get;set;}
    public string RowKey {get;set;}
    public string Content {get;set;}
}
