using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SoilIngestion.Entity;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
namespace Samples.SoilIngestion
{
    public static class SoilIngestion
    {
        [FunctionName("SoilIngestion")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous,"post", Route = null)] HttpRequest req,
            [CosmosDB(
             databaseName: "soildatastore",
             collectionName: "soilobservations",
            ConnectionStringSetting = "CosmosDbConnectionString", CreateIfNotExists=true)]IAsyncCollector<dynamic> documentsOut,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Get the request body to parse the soil data
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            
            var response = await AddDocument(requestBody, documentsOut, log);

             return response != null
             ? (ActionResult)new OkObjectResult(response)
                 : new BadRequestObjectResult("Could not store document");

        }

        async static Task<string> AddDocument(dynamic document, IAsyncCollector<dynamic> documentsOut, ILogger log)
        {
            
            var successMessage = "Item added to CosmosDB successfully!";
            Soil data = JsonConvert.DeserializeObject<Soil>(document);
            var documentId = System.Guid.NewGuid().ToString();
            data.FarmerId = documentId;

            // Add a JSON document to the output container.
            await documentsOut.AddAsync(data);
            log.LogInformation("Item with document id {0} added to cosmosDB", documentId);
            return await Task.FromResult<string>(successMessage);
        }
    }
}
