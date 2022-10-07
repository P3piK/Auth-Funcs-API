using AuthFuncsCore.Config;
using AuthFuncsWorkerService.Dto;
using AuthFuncsWorkerService.Interface;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AuthFuncsWorkerService
{
    public enum EmailWorkerActionName
    {
        PasswordReset,
    }

    public class EmailWorker : BackgroundService, INotificationService, IDisposable
    {
        static ServiceBusClient client;
        static ServiceBusSender sender;

        public ILogger<EmailWorker> Logger { get; }

        public EmailWorker(ILogger<EmailWorker> logger, BusServiceConfig busServiceConfig)
        {
            Logger = logger; 
            
            var clientOptions = new ServiceBusClientOptions() { TransportType = ServiceBusTransportType.AmqpWebSockets };
            client = new ServiceBusClient(busServiceConfig.ConnectionString, clientOptions);
            sender = client.CreateSender(busServiceConfig.EmailQueueName);
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

        public async Task SendNotificationAsync(string recipient, EmailWorkerActionName action)
        {
            using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();
            var body = JsonSerializer.Serialize(
                new NotificationDto() 
                { 
                    Recipient = recipient, 
                    Action = action.ToString() 
                });
            
            if (!messageBatch.TryAddMessage(new ServiceBusMessage(body)))
            {
                throw new Exception($"Failed to add message to Bus for {recipient}");
            }

            try
            {
                await sender.SendMessagesAsync(messageBatch);
                Console.WriteLine($"Added message to queue successfully!");
                Logger.LogInformation($"Added message to queue: {body}");
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

            base.Dispose();
        }
    }
}