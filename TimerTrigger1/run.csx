using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Http;
public static async Task Run(TimerInfo myTimer, ILogger log)
{
        var queryString = "select top 1  ArchiveLogId from [dbo].[ArchiveLog]";
         string dataRow= null;
         string result = null;
        string conn =  System.Environment.GetEnvironmentVariable("connectionString", EnvironmentVariableTarget.Process);
                using (SqlConnection connection = new SqlConnection(conn))
                {
               
                    SqlCommand command = new SqlCommand(queryString, connection);

                    try
                    {
                        connection.Open(); 
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            dataRow = string.Format("{0}", reader[0]);
                        }
                        reader.Close();

                        log.LogInformation($"The dataRow: {dataRow}");
                        var client = new HttpClient();
                        string url = "http://dummy.restapiexample.com/api/v1/employee/";
                        var response = await client.GetAsync(string.Concat(url,dataRow));
                        result = await response.Content.ReadAsStringAsync();
                        log.LogInformation($"The result of calling web service: {result}");
                    }
                    catch (Exception ex)
                    {
                        log.LogInformation(ex.Message);
                    }

                }
}
