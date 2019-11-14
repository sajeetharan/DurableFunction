using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace GabDemo.Pattern5.HumanInteraction
{
    public static class ValidateEmail
    {
        //[FunctionName("ValidateEmail")]
        //public static async Task<HttpResponseMessage> Run(
        //    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]HttpRequestMessage req,
        //    [OrchestrationClient] DurableOrchestrationClient client)
        //{
        //    string userId = req.GetQueryNameValuePairs()
        //         .FirstOrDefault(q => string.Compare(q.Key, "userId", true) == 0)
        //         .Value;

        //    string instanceId = req.GetQueryNameValuePairs()
        //         .FirstOrDefault(q => string.Compare(q.Key, "userId", true) == 0)
        //         .Value;

        //    await client.RaiseEventAsync($"UserVerifiedEmail{userId}")

        //    return req.CreateResponse(HttpStatusCode.OK);
        //}
    }
}
