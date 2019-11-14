using GabDemo.Pattern5.HumanInteraction.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace GabDemo.Pattern5.HumanInteraction
{
    public static class RegisterUserApi
    {
        [FunctionName("RegisterUser")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequestMessage req,
            [OrchestrationClient] DurableOrchestrationClient client)
        {
            // Get request body
            var userData = await req.Content.ReadAsAsync<User>();

            var instanceId = await client.StartNewAsync("RegisterUserWorkflow", userData);

            return client.CreateCheckStatusResponse(req, instanceId);
        }
    }
}