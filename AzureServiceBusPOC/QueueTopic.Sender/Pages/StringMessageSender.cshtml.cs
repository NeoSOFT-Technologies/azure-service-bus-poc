using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QueueTopic.Sender.Services;

namespace QueueTopic.Sender.Pages
{
    public class StringMessageSenderModel : PageModel
    {
        public string message = "";
        public readonly IQueueService _queueService;
        public readonly ITopicService _topicService;

        public StringMessageSenderModel(IQueueService queueService, ITopicService topicService)
        {
            _queueService = queueService;
            _topicService = topicService;
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            message = Request.Form["message"];

            // Queue
            //_queueService.SendMessageAsync(message);

            // Topic
            _topicService.SendMessageAsync(message);
        }
    }
}
