using Microsoft.Azure.ServiceBus;
using System.Text;
using System.Text.Json;

namespace QueueTopic.Sender.Services
{
    public class QueueService : IQueueService
    {
        private readonly IConfiguration _config;

        public QueueService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendMessageAsync<T>(T serviceBusMessage)
        {
            var queueClient = new QueueClient(_config.GetConnectionString("QueueConnectionStrings"), _config.GetConnectionString("QueueName"));
            string messageBody = JsonSerializer.Serialize(serviceBusMessage);
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));

            // await queueClient.SendAsync(message);

            ////Scheduled Message

            DateTimeOffset scheduleTime = DateTime.UtcNow.AddMinutes(2);
            await queueClient.ScheduleMessageAsync(message, scheduleTime);
        }
    }
}
