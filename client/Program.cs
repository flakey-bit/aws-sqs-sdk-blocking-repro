using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string url = "http://localhost:5000/";
            // var client = new HttpClient()
            // {
            //     Timeout = TimeSpan.FromSeconds(5)
            // };
            //
            // var request = new HttpRequestMessage
            // {
            //     Method = HttpMethod.Post,
            //     RequestUri = new Uri(url)
            // };

            var client = new AmazonSQSClient(new AmazonSQSConfig
            {
                ServiceURL = url,
                Timeout = TimeSpan.FromSeconds(5),
                UseHttp = true,
                MaxErrorRetry = 0
            });

            var request = new ReceiveMessageRequest()
            {
                WaitTimeSeconds = 10,

            };

            var sw = Stopwatch.StartNew();
            var response = await client.ReceiveMessageAsync(request);

            Console.Out.WriteLine($"{sw.Elapsed} Got messages: \n{response.Messages.Count}");
        }
    }
}
