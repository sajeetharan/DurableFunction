using GabDemo.Pattern2.FanOutFanIn.Models;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GabDemo.Pattern2.FanOutFanIn.Activities
{
    public static class GetWeatherFromApi
    {
        private class ExternalWeatherResponse
        {
            public string Applicable_Date { get; set; }

            public decimal Max_Temp { get; set; }

            public decimal Min_Temp { get; set; }

            public decimal Predictability { get; set; }

            public decimal The_Temp { get; set; }

            public string Weather_State_Name { get; set; }
        }

        [FunctionName("GetWeatherFromApi")]
        public static async Task<WeatherDto> Run([ActivityTrigger] DurableActivityContext activityContext)
        {
            var date = activityContext.GetInput<DateTime>();

            using (var httpClient = new HttpClient())
            {
                var baseUrl = "https://www.metaweather.com/api/location/851128/";

                var response = await httpClient.GetAsync($"{baseUrl}{date.ToString("yyyy/MM/dd")}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = JsonConvert.DeserializeObject<IEnumerable<ExternalWeatherResponse>>(await response.Content.ReadAsStringAsync());

                    var weather = responseContent.OrderByDescending(x => x.Predictability).First();

                    return new WeatherDto
                    {
                        AvgTemp = weather.The_Temp,
                        Date = weather.Applicable_Date,
                        MaxTemp = weather.Max_Temp,
                        MinTemp = weather.Min_Temp,
                        Weather = weather.Weather_State_Name
                    };
                }
            }

            return null;
        }
    }
}