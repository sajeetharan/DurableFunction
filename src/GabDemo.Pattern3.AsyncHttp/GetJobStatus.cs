using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace GabDemo.Pattern3.AsyncHttp
{
    public static class GetJobStatus
    {
        [FunctionName("GetJobStatus")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequestMessage req,
            [OrchestrationClient] DurableOrchestrationClient client)
        {
            string instanceId = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "instanceId", true) == 0)
                .Value;

            var durableJobStatus = await client.GetStatusAsync(instanceId);

            HttpStatusCode returnStatusCode = HttpStatusCode.InternalServerError;

            // parse appropriate status code
            switch (durableJobStatus.RuntimeStatus)
            {
                case OrchestrationRuntimeStatus.Canceled:
                    returnStatusCode = HttpStatusCode.NoContent;
                    break;

                case OrchestrationRuntimeStatus.Completed:
                    returnStatusCode = HttpStatusCode.OK;
                    break;

                case OrchestrationRuntimeStatus.Pending:
                    returnStatusCode = HttpStatusCode.Created;
                    break;

                case OrchestrationRuntimeStatus.ContinuedAsNew:
                case OrchestrationRuntimeStatus.Running:
                    returnStatusCode = HttpStatusCode.Accepted;
                    break;

                case OrchestrationRuntimeStatus.Failed:
                    returnStatusCode = HttpStatusCode.InternalServerError;
                    break;

                case OrchestrationRuntimeStatus.Terminated:
                    returnStatusCode = HttpStatusCode.ServiceUnavailable;
                    break;
            }

            return req.CreateResponse(returnStatusCode);
        }
    }
}
