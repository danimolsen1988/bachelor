using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client.Exceptions;
using Microsoft.Azure.Devices.Common.Exceptions;

namespace SimulatorTest
{
    class DeviceManager
    {
        static string connectionString;
        static RegistryManager registryManager;

        public static string HostName { get; set; }

        public static void IoTHubConnect(string connString) { 
            connectionString = connString;

            // Create an instance of the RegistryManager from the IoT Hub connection string.
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);

            var builder = IotHubConnectionStringBuilder.Create(connString);

            HostName = builder.HostName;
        }

        public static async Task<string> RegisterDeviceAsync(string connectionString, string deviceId) {

            //Connect
            if (registryManager == null) {
                IoTHubConnect(connectionString);
            }

            //create a device
            var device = new Device(deviceId) {
                Status = DeviceStatus.Enabled
            };

            try {
                device = await registryManager.AddDeviceAsync(device);
            }
            catch (DeviceAlreadyExistsException ExistsException) {
                //If device exists, get it instead and make sure its enabled
                device = await registryManager.GetDeviceAsync(deviceId);
                device.Status = DeviceStatus.Enabled;
                await registryManager.UpdateDeviceAsync(device);
            }
            catch (Exception ex) {
                //write error to console
                Console.WriteLine(ex);
            }
            //return device key
            return device.Authentication.SymmetricKey.PrimaryKey;
        }
    }
}
