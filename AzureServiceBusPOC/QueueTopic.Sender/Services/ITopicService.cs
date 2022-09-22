using QueueTopic.Sender.Models;

namespace QueueTopic.Sender.Services
{
    public interface ITopicService
    {
        Task PublishMessage<T>(Employee serviceBusMessage);

        Task SendMessageAsync<T>(T serviceBusMessage);
    }
}
