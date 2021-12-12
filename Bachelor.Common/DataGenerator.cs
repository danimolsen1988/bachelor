using Bachelor.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Bachelor.Common
{
    public static class DataGenerator
    {
        private static readonly Random random = new Random();

        public static OpsDeviceTelemetry GenerateData() { 
            
            OpsDeviceTelemetry opsDeviceTelemetry = new OpsDeviceTelemetry();
            opsDeviceTelemetry.deviceId = GetDeviceId();
            opsDeviceTelemetry.timestamp = DateTime.UtcNow;            
            opsDeviceTelemetry.measurements = new List<SubDeviceTelemetry>();
            //important that this is consistent
            opsDeviceTelemetry.partitionKey = $"{opsDeviceTelemetry.deviceId}-{DateTime.UtcNow:yyyy-MM-dd}";

            foreach (var item in GetAllSeedData())
            {
                opsDeviceTelemetry.measurements.Add(GenerateSubDeviceTelemetry(item.id, item.initialValue));
            }

            return opsDeviceTelemetry;                
        }

        /// <summary>
        /// Generates data for a subdevice, 
        /// </summary>
        /// <param name="subId">The Id for subdevice</param>
        /// <param name="initialValue">The initial value, ensures somewhat identical measurements</param>
        /// <param name="deviationPercentage">The percentage deviation from initial value</param>
        /// <returns></returns>
        private static SubDeviceTelemetry GenerateSubDeviceTelemetry(string subId, double initialValue, double deviationPercentage = 0.05) {

            var upper = initialValue + (initialValue * deviationPercentage);
            var lower = initialValue - (initialValue * deviationPercentage);
            
            return new SubDeviceTelemetry() { subId = subId, measurement = random.NextDouble() * (upper - lower) + lower };            
        }

        /// <summary>
        /// Retrieves Subdevice ids and their initial value
        /// </summary>
        /// <returns></returns>
        private static List<SeedData> GetAllSeedData() {
            //SubDevice ids needs to match those predefined in database!
            //SubDevice ids should already exist in the database
            var seedData = new List<SeedData>();
            seedData.Add(new SeedData() { id = "subDevice_1", initialValue = 25.0 }); //temp
            seedData.Add(new SeedData() { id = "subDevice_2", initialValue = 10.0 }); //temp
            seedData.Add(new SeedData() { id = "subDevice_3", initialValue = 32.0 }); //psi
            seedData.Add(new SeedData() { id = "subDevice_4", initialValue = 50.0 }); //temp
            seedData.Add(new SeedData() { id = "subDevice_5", initialValue = 1500.0 }); //rpm
            seedData.Add(new SeedData() { id = "subDevice_6", initialValue = 900.0 }); //rpm
            seedData.Add(new SeedData() { id = "subDevice_7", initialValue = 15.0 }); //temp
            seedData.Add(new SeedData() { id = "subDevice_8", initialValue = 240.0 }); //temp
            seedData.Add(new SeedData() { id = "subDevice_9", initialValue = 12.0 }); //psi
            seedData.Add(new SeedData() { id = "subDevice_10", initialValue = 95.0 }); //temp
            return seedData;
        }

        //Device id must match those in database
        public static string GetDeviceId() {
            return "ops_device_1";
        }
    
    }
}
