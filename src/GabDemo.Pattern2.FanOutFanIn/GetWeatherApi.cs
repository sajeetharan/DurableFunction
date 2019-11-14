using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GabDemo.Pattern2.FanOutFanIn
{
    public static class GetWeatherApi
    {
        [FunctionName("GetWeather")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequestMessage req,
            [OrchestrationClient] DurableOrchestrationClient client)
        {
            string startDate = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "startDate", true) == 0)
                .Value;

            var instanceId = await client.StartNewAsync("ThreeDayWeather", Convert.ToDateTime(startDate));

            return client.CreateCheckStatusResponse(req, instanceId);
        }
    }
}