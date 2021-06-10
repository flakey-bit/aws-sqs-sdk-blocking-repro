using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace server.Controllers
{
    [ApiController]
    [Route("/")]
    public class SQSController : ControllerBase
    {
        public SQSController(ILogger<SQSController> logger)
        {
        }

        [HttpPost]
        public async Task<EmptyResult> Post()
        {
            Response.ContentType = "text/xml";
            string xml = "<ReceiveMessageResponse>\n  <ReceiveMessageResult />\n  <ResponseMetadata>\n    <RequestId>b6633655-283d-45b4-aee4-4e84e0ae6afa</RequestId>\n  </ResponseMetadata>\n</ReceiveMessageResponse>";
            var bytes = Encoding.UTF8.GetBytes(xml);

            Response.Headers.ContentLength = bytes.Length;
            await Response.Body.FlushAsync();

            while (true) ;
            //await Task.Delay(TimeSpan.FromSeconds(5));
            //await new MemoryStream(bytes).CopyToAsync(Response.Body);
            return new EmptyResult(); // Unreachable
        }
    }
}
