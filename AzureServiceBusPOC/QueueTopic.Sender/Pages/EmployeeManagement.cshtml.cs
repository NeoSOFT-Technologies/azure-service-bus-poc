using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QueueTopic.Sender.Models;
using QueueTopic.Sender.Services;

namespace QueueTopic.Sender.Pages
{
    public class EmployeeManagementModel : PageModel
    {
        public string firstName = "";
        public string lastName = "";

        public readonly IQueueService _queueService;
        public readonly ITopicService _topicService;

        public EmployeeManagementModel(IQueueService queueService, ITopicService topicService)
        {
            _queueService = queueService;
            _topicService = topicService;
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            firstName = Request.Form["firstname"];
            lastName = Request.Form["lastname"];

            var employeeCreated = new Employee
            {
                FirstName = firstName,
                LastName = lastName
            };

            // Queue
            // _queueService.SendMessageAsync(employeeCreated);

            // Topic
             _topicService.PublishMessage<Employee>(employeeCreated);
        }
    }
}
