using GabDemo.Pattern2.FanOutFanIn.Models;
using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GabDemo.Pattern2.FanOutFanIn
{
    public static class ThreeDayWeather
    {
        [FunctionName("ThreeDayWeather")]
        public static async Task<IEnumerable<WeatherDto>> Run([OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var date = context.GetInput<DateTime>();

            var requests = new Task<WeatherDto>[3];

            // fan out
            for (var i = 0; i < 3; i++)
            {
                requests[i] = context.CallActivityAsync<WeatherDto>("GetWeatherFromApi", date.AddDays(i));
            }

            // fan in
            await Task.WhenAll(requests);

            // aggregate results
            return requests.Select(x => x.Result).Where(x => x != null);
        }
    }
}