namespace SimulatorTest
{
    internal class Program
    {

        // AutoResetEvent to signal when to exit the application.
        private static readonly AutoResetEvent WaitHandle = new AutoResetEvent(false);


        static async Task Main(string[] args) {
            Console.WriteLine("Hello, World!");

            // Handle Control+C or Control+Break.
            Console.CancelKeyPress += (o, e) =>
            {
                WriteLineInColor("Stopped generator. No more events are being sent.", ConsoleColor.Yellow);
                CancelAll();

                // Allow the main thread to continue and exit...
                WaitHandle.Set();
            };

            try
            {
                var deviceKey = await DeviceManager.RegisterDeviceAsync("iothubconnstring", "deviceId");


            }
            catch (OperationCanceledException)
            {
                //stopping
            }

        }
    }

}


