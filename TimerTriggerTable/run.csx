using System;

public static void Run(TimerInfo myTimer,ICollector<Person> outputTable, ILogger log)
{
    log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

    for (int i = 1; i < 10; i++)
        {
            log.LogInformation($"Adding Person entity {i}");
            outputTable.Add(
                new Person() { 
                    PartitionKey = "Test", 
                    RowKey = i.ToString(), 
                    Name = "Name" + i.ToString() }
                );
        }
}


public class Person
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public string Name { get; set; }
}