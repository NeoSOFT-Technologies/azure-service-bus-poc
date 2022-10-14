using Microsoft.Azure.ServiceBus;
using System.Text;

class Program
{
    static ISubscriptionClient subscriptionClient;
    static void Main(string[] args)
    {
        string sbConnectionString = "Endpoint=sb://projectrajgad.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=2WRfFVBXKu+0hq/k1gVIUKHcaxlVmh4Os6qKsHDRiSA=";
        string sbTopic = "offers";
        string sbSubscription = "Subscriber2";
        try
        {
            subscriptionClient = new SubscriptionClient(sbConnectionString, sbTopic, sbSubscription);

            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            subscriptionClient.RegisterMessageHandler(ReceiveMessagesAsync, messageHandlerOptions);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Console.ReadKey();
            subscriptionClient.CloseAsync();
        }
    }

    static async Task ReceiveMessagesAsync(Message message, CancellationToken token)
    {
        Console.WriteLine($"Subscribed message: {Encoding.UTF8.GetString(message.Body)}");

        await subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
    }

    static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
    {
        Console.WriteLine(exceptionReceivedEventArgs.Exception);
        return Task.CompletedTask;
    }
}