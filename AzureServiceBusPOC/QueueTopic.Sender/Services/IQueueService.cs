namespace QueueTopic.Sender.Services
{
    public interface IQueueService
    {
        Task SendMessageAsync<T>(T serviceBusMessage);
    }
}
