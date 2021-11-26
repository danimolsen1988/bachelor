using Bachelor.Common;
using Bachelor.Common.Models;
//using SimulatorTest.DataModels;
//using Bachelor.Common.DataModels;
using System.Linq;

namespace SimulatorTest
{
    internal class Program
    {

        // AutoResetEvent to signal when to exit the application.
        private static readonly AutoResetEvent WaitHandle = new AutoResetEvent(false);
        private static SimulatedDevice? _simulatedDevice;

        private static string connString = "HostName=iot-hub-bachelor.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=FwhPgFZp4Ei98rCj7EQHwZdwtD268r/Jjbul3LMcKfk=";

        private static readonly object _lock = new object();

        static async Task Main(string[] args) {
            
            Console.WriteLine("Simulation tool started!");

            //Handle Control+C or Control+Break.
            Console.CancelKeyPress += (o, e) =>
            {
                Console.WriteLine("Stopped generator");
                Cancel();

               // Allow the main thread to continue and exit...
               WaitHandle.Set();
            };

            try
            {
                //check if the device is white listed
                if (Helpers.IsDeviceWhiteListed(DataGenerator.GetDeviceId())){
                    //Make sure devices exists in iothub - Only using a single device for simulated data
                    var deviceKey = await DeviceManager.RegisterDeviceAsync(connString, DataGenerator.GetDeviceId());
                    _simulatedDevice = new SimulatedDevice(DeviceManager.HostName, DataGenerator.GetDeviceId(), deviceKey);
                    await _simulatedDevice.RunSimulationAsync();
                }
                else {
                    Console.WriteLine("Simulation tool started!");
                }
                
            }
            catch (OperationCanceledException)
            {
                //stopping
                Console.WriteLine("Canceled");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Cancel();
            Console.WriteLine("Done!");
        }

        private static void Cancel() {
            _simulatedDevice?.CancelSimulation();
        }

        public static void PrintConsole(string msg) {
            lock (_lock) {
                Console.WriteLine(msg);
            }
        }
    }

}


