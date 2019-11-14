using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace GabDemo.Pattern1.FunctionChaining
{
    public static class HelloWorldApi
    {
        [FunctionName("HelloWorld")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]HttpRequestMessage req,
            [OrchestrationClient] DurableOrchestrationClient client)
        {
            var instanceId = await client.StartNewAsync("HelloWorkflow", null);

            return client.CreateCheckStatusResponse(req, instanceId);
        }
    }
}