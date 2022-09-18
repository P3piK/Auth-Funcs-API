using AuthFuncsWorkerService.Interface;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace AuthFuncsWorkerService
{
    public class EmailWorker : BackgroundService, INotificationService, IDisposable
    {
        static string connectionString;
        static string queueName = "authfuncsemailqueue";

        static ServiceBusClient client;
        static ServiceBusSender sender;

        public ILogger<EmailWorker> Logger { get; }

        public EmailWorker(ILogger<EmailWorker> logger)
        {
            Logger = logger; 
            
            var clientOptions = new ServiceBusClientOptions() { TransportType = ServiceBusTransportType.AmqpWebSockets };
            client = new ServiceBusClient(connectionString, clientOptions);
            sender = client.CreateSender(queueName);
        }

        protected override async Task<Task> ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Email Worker started!", ConsoleColor.Green);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(5000);
            }

            return Task.CompletedTask;
        }

        public async Task SendNotificationAsync(string recipient, string message)
        {
            using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

            try
            {
                if (!messageBatch.TryAddMessage(new ServiceBusMessage($"Hello {recipient}")))
                {
                    throw new Exception($"Failed to add message to Bus for {recipient}");
                }    

                Console.WriteLine($"Notification sent successfully to {recipient}!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while notifying {recipient}!", ConsoleColor.Red);
                Logger.LogError(e, e.Message);
            }
        }

        public void Dispose()
        {
            sender.DisposeAsync();
            client.DisposeAsync();
            Console.WriteLine("Disposed EmailWorker");
        }
    }
}