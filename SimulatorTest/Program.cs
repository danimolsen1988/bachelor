using Bachelor.Common;
using Bachelor.Common.Models;

namespace SimulatorTest
{
    internal class Program
    {

        // AutoResetEvent to signal when to exit the application.
        private static readonly AutoResetEvent WaitHandle = new AutoResetEvent(false);
        private static SimulatedDevice? _simulatedDevice;

        private static readonly object _lock = new object();

        static async Task Main(string[] args) {
            Console.WriteLine("Hello, World!");

            // Handle Control+C or Control+Break.
            Console.CancelKeyPress += (o, e) =>
            {                
                Console.WriteLine("Stopped generator");
                Cancel();

                // Allow the main thread to continue and exit...
                WaitHandle.Set();
            };

            try
            {
                //Make sure devices exists in iothub - Only using a single device for simulated data
                var deviceKey = await DeviceManager.RegisterDeviceAsync("iothubconnstring", DataGenerator.GetDeviceId());

                //SimulatedDevice simulatedDevice = new SimulatedDevice(DeviceManager.HostName, "deviceID", deviceKey);
                _simulatedDevice = new SimulatedDevice(DeviceManager.HostName, DataGenerator.GetDeviceId(), deviceKey);

                //I should print the information that is created!!!

                await _simulatedDevice.RunSimulationAsync();


                // await simulatedDevice.RunSimulationAsync();
            }
            catch (OperationCanceledException)
            {
                //stopping
                Console.WriteLine("Canceled");
            }
            catch (Exception ex) {
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


