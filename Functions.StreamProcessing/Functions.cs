using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Bachelor.Common;
using Bachelor.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;


namespace Functions.StreamProcessing
{
    public class Functions
    {        
        
        [FunctionName(nameof(IoTHubTrigger))]
        public static async Task IoTHubTrigger([IoTHubTrigger("messages/events", Connection = "IoTHubConnection")]EventData[] messages, [CosmosDB(
                databaseName: "powercon",
                collectionName: "telemetry",
                ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<OpsDeviceTelemetry> opsTelemetryOut, ILogger log)
        {
            var exceptions = new List<Exception>();
            log.LogInformation($"IoT Hub trigger function processed a message");

            foreach (var message in messages) {
                try
                {
                    //var messageBody = Encoding.UTF8.GetString(message.Body.ToArray, message.Body.Offset, message.Body.Count);
                    var messageBody = message.EventBody.ToString();

                    var data = JsonConvert.DeserializeObject<OpsDeviceTelemetry>(messageBody);

                    //We have the data as object !!

                    //set partitionkey
                    //then save to database!
                    data.partitionKey = $"{data.deviceId}--{DateTime.UtcNow:yyyy-MM-dd}";
                    //data.timestamp = DateTime.UtcNow; // not sure i should do it here?
                    await opsTelemetryOut.AddAsync(data);

                }
                catch (Exception e)
                {
                    exceptions.Add(e);
                }
            }

            if (exceptions.Count > 1) { 
                throw new AggregateException(exceptions);
            }

            if (exceptions.Count == 0) {
                throw exceptions.Single();
            }
        }
    }
}