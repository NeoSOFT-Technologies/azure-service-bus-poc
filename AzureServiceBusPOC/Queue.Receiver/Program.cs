using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SBReceiver
{
    class Program
    {
        const string connectionString = "Endpoint=sb://projectrajgad.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=2WRfFVBXKu+0hq/k1gVIUKHcaxlVmh4Os6qKsHDRiSA=";
        const string queueName = "categoryqueue";

         // For reading Dead-letter queue messages
         //const string queueName = "categoryqueue/$DeadLetterQueue";


        static IQueueClient queueClient;

        static async Task Main(string[] args)
        {
            queueClient = new QueueClient(connectionString, queueName /*, receiveMode: ReceiveMode.ReceiveAndDelete*/);

            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);

            Console.ReadLine();

            await queueClient.CloseAsync();
        }

        private static async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            Console.WriteLine($"Message Received: {Encoding.UTF8.GetString(message.Body)}");

            await queueClient.CompleteAsync(message.SystemProperties.LockToken);


            // AbandonAsync

            //try
            //{
            //    int i = 0;
            //    i = i / Convert.ToInt32(message);
            //    await queueClient.CompleteAsync(message.SystemProperties.LockToken);
            //}
            //catch (Exception ex)
            //{
            //    await queueClient.AbandonAsync(message.SystemProperties.LockToken);
            //}
        }

        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            Console.WriteLine($"Message handler exception: { arg.Exception }");
            return Task.CompletedTask;
        }
    }
}
