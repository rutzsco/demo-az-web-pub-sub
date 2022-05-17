
using Azure.Messaging.WebPubSub;
using System.Text.Json;
using Websocket.Client;

namespace Demo.Subscriber
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: subscriber <connectionString> <hub>");
                return;
            }
            var connectionString = args[0];
            var hub = args[1];
            var clientId = Console.ReadLine();

            var serviceClient = new WebPubSubServiceClient(connectionString, hub);
            var url = serviceClient.GetClientAccessUri(userId: clientId);


            using (var client = new WebsocketClient(url))
            {
                // Disable the auto disconnect and reconnect because the sample would like the client to stay online even no data comes in
                client.ReconnectTimeout = null;
                client.MessageReceived.Subscribe(msg => ProcessEvent(msg));
                await client.Start();
                Console.WriteLine($"Connected. {client.Name}");
                Console.Read();
            }
        }

        private static void ProcessEvent(ResponseMessage responseMessage)
        {
            var pce = JsonSerializer.Deserialize<ProcessCompletedEvent>(responseMessage.Text);
            Console.WriteLine($"Message received - Id: {pce.Id} ProcessId: {pce.ProcessId}");
        }
    }
}