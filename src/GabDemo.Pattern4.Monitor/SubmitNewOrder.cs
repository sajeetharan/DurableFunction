using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace GabDemo.Pattern4.Monitor
{
    public static class SubmitNewOrder
    {
        [FunctionName("SubmitNewOrder")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequestMessage req,
            [OrchestrationClient] DurableOrchestrationClient client)
        {
            // Get request body
            dynamic orderData = await req.Content.ReadAsAsync<object>();

            var newOrderJobId = await client.StartNewAsync("SubmitNewOrder", orderData);

            await client.StartNewAsync("OrderMonitor", newOrderJobId);

            return req.CreateResponse(HttpStatusCode.OK, "Order is submitted. You will be notified upon its completion.");
        }
    }
}