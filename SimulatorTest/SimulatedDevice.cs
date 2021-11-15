using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bachelor.Common.Models;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

namespace SimulatorTest
{
    /// <summary>
    /// ConfigureAwait(false) configures the task so that continuation after the await does not have to be run in the caller context, therefore avoiding any possible deadlocks.
    /// </summary>
    public class SimulatedDevice
    {
        // The amount of time to delay between sending telemetry.
        private readonly TimeSpan CycleTime = TimeSpan.FromMilliseconds(5000);

        private DeviceClient _DeviceClient;
        private string _IotHubUri { get; set; }
        public string DeviceId { get; set; }
        public string DeviceKey { get; set; }
        private readonly CancellationTokenSource _localCancellationSource = new CancellationTokenSource();

        public SimulatedDevice(string iotHubUri, string deviceId, string deviceKey) { 
            _IotHubUri = iotHubUri;
            DeviceId = deviceId;
            DeviceKey = deviceKey;
            _DeviceClient = DeviceClient.Create(_IotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey(DeviceId, DeviceKey));
        }

        public async Task RunSimulationAsync() {
            await SendDataToHub(_localCancellationSource.Token).ConfigureAwait(false);
        }

        public void CancelSimulation() { 
            _localCancellationSource.Cancel();
        }

        private async Task SendDataToHub(CancellationToken cancellationToken) {
            //TODO 1
            while (!_localCancellationSource.IsCancellationRequested) {
                OpsDevice data = new OpsDevice();
                data.deviceId = "";
                data.measurements = new List<SubDevice>();
                data.measurements.Add(new SubDevice() { subId = "", measurement = 1 });

                await SendEvent(JsonConvert.SerializeObject(data), cancellationToken).ConfigureAwait(false);
                await Task.Delay(CycleTime);
            }
        }

        private async Task SendEvent(string message, CancellationToken cancellationToken) {

            using (var eventData = new Message(Encoding.ASCII.GetBytes(message))) {
                // Send telemetry to IoT Hub. All messages are partitioned by the Device Id, guaranteeing message ordering.
                var sendEventAsync = _DeviceClient?.SendEventAsync(eventData, cancellationToken);
                if (sendEventAsync != null) {
                    await sendEventAsync.ConfigureAwait(false);
                }
            }
        }
    }
}
