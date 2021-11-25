using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventHubs;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using System.Linq;
using testfunction.Models;

namespace testfunction
{
    public static class Function1
    {
        private static HttpClient client = new HttpClient();

        [FunctionName(nameof(IoTHubTrig))]
        public static async Task IoTHubTrig([IoTHubTrigger("messages/events", Connection = "IoTHubConnection")]EventData[] messages, 
            [CosmosDB(databaseName:"powercon",collectionName:"telemetry",ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<OpsDeviceTelemetry> opsTelemetryOut, ILogger log)
        {
            var exceptions = new List<Exception>();
            log.LogInformation($"C# IoT Hub trigger function processing: {messages.Length}");

            foreach (var message in messages)
            {
                try
                {                    
                    var messageBody = Encoding.UTF8.GetString(message.Body.Array, message.Body.Offset, message.Body.Count);
                    var telemetry = JsonConvert.DeserializeObject<OpsDeviceTelemetry>(messageBody);

                    //SAVE TO DATABASE
                    await opsTelemetryOut.AddAsync(telemetry);
                    
                }
                catch (Exception e)
                {

                    exceptions.Add(e);
                }

                if (exceptions.Count > 1) {
                    throw new AggregateException(exceptions);
                }

                if (exceptions.Count == 1) {
                    throw exceptions.Single();
                }
            }
        }
    }
}