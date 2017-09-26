using System;
using System.IO;
using System.Net;
using System.Net.Http;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using TIK.Applications.DataTransformation.Commands.FixedLength;

namespace TIK.ProcessService.Hangfire.DataTransformation.Controllers
{
    [Route("api/DataTransform")]
    public class DataTransformController : Controller
    {
        public DataTransformController()
        {
        }

        [Route("Transform/{template}")]
        public HttpResponseMessage Transform(string template)
        {

 
            try
            {
                var req = new FixedLengthDto() { FileName = template };
                using (var ms = new MemoryStream(2048))
                {
                    var task = Request.Body.CopyToAsync(ms);
                    task.Wait();
                    req.DataResult = ms.ToArray();  // returns base64 encoded string JSON result
                }


                var jobId = BackgroundJob.Enqueue<IFixedLengthCommand>(x => x.Transform(req));

                BackgroundJob.ContinueWith<IFixedLengthCommand>(jobId, x => x.CallBack(req));

            }
            catch (IOException)
            {
                throw new HttpRequestException(HttpStatusCode.InternalServerError.ToString());
            }

            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.Created;
            return response;
        }
    }
}
