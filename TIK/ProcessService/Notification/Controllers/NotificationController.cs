using System;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using TIK.Domain.Notifications;
using Hangfire;
using TIK.Applications.Notification.Commands.Socket;

namespace TIK.ProcessService.Notification.Controllers
{
    public class NotificationController : Controller
    {
        public NotificationController()
        {
        }

        [HttpPut]
        [Route("Socket")]
        public IActionResult Socket([FromBody]SignalRCommand inputModel)
        {

            var jobId = BackgroundJob.Enqueue<ISignalRNotificationCommand>(x => x.Send(inputModel));

            return Ok(jobId);
        }
    }
}
