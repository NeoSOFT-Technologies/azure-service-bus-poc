using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using QueueTopic.Sender.Models;
using System.Text;

namespace QueueTopic.Sender.Services
{
    public class TopicService : ITopicService
    {
        private readonly IConfiguration _config;

        public TopicService(IConfiguration config)
        {
            _config = config;
        }

        public async Task PublishMessage<T>(Employee serviceBusMessage)
        {
            var topicClient = new TopicClient(_config.GetConnectionString("TopicConnectionStrings"), _config.GetConnectionString("TopicName"));
            string messageBody = JsonConvert.SerializeObject(serviceBusMessage);
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));
            message.UserProperties["messageType"] = typeof(T).Name;
            message.UserProperties.Add("FirstName", serviceBusMessage.FirstName);
            message.UserProperties.Add("LastName", serviceBusMessage.LastName);
            await topicClient.SendAsync(message);
        }

        public async Task SendMessageAsync<T>(T serviceBusMessage)
        {
            var topicClient = new TopicClient(_config.GetConnectionString("TopicConnectionStrings"), _config.GetConnectionString("TopicName"));
            string messageBody = System.Text.Json.JsonSerializer.Serialize(serviceBusMessage);
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));
            message.UserProperties["messageType"] = "Raw";
            await topicClient.SendAsync(message);
        }
    }
}
