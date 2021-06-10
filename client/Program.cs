using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string url = "http://localhost:5000/";
            var client = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(5)
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url)
            };

            var sw = Stopwatch.StartNew();
            var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            Console.Out.WriteLine($"{sw.Elapsed} Got headers: \n{response.Headers}");

            var body = await response.Content.ReadAsStringAsync();
            Console.Out.WriteLine($"{sw.Elapsed} Got body: \n{body}");
        }
    }
}
