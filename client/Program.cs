using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
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

            //var response = await client.ReceiveMessageAsync(request);

            var getSqsTimeout = CancellationTokenSource.CreateLinkedTokenSource(new CancellationToken());
            getSqsTimeout.CancelAfter(TimeSpan.FromSeconds(5));

            try
            {
                var response = await client.ReceiveMessageAsync(request, getSqsTimeout.Token).ConfigureAwait(false);
                Console.Out.WriteLine($"{sw.Elapsed} Got messages: \n{response.Messages.Count}");
            }
            catch (TaskCanceledException exception) when (getSqsTimeout.IsCancellationRequested)
            {
                Console.Out.WriteLine("GetSqsMessages canceled.");
            }
            catch (OperationCanceledException)
            {
                Console.Out.WriteLine("GetSqsMessages canceled.");
            }


        }
    }
}
