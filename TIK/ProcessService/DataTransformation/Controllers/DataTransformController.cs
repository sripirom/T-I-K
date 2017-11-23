using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using TIK.Applications.DataTransformation.Commands.FixedLength;

namespace TIK.ProcessService.DataTransformation.Controllers
{
    [Route("api/DataTransform")]
    public class DataTransformController : Controller
    {
        public DataTransformController()
        {
        }

        [Route("Transform/{template}")]
        [ValidateAntiForgeryToken]
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
