using AuthFuncsWorkerService.Interface;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AuthFuncsWorkerService
{
    public class EmailWorker : BackgroundService, INotificationService
    {
        public ILogger<EmailWorker> Logger { get; }

        public EmailWorker(ILogger<EmailWorker> logger)
        {
            Logger = logger;
        }

        protected override async Task<Task> ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Email Worker started!", ConsoleColor.Green);

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("TEST");
                await Task.Delay(5000);
            }

            return Task.CompletedTask;
        }

        public void SendNotification(string recipient, string message)
        {
            try
            {
                // send notification
                Console.WriteLine($"Notification sent successfully to {recipient}!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while notifying {recipient}!", ConsoleColor.Red);
                Logger.LogError(e, e.Message);
            }
        }
    }
}