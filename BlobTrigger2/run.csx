public static void Run(Stream myBlob, string name, ILogger log)
{
    log.LogInformation($"C# Blob trigger function Processedsdssd11 blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
}
