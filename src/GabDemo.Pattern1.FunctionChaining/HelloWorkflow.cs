using Microsoft.Azure.WebJobs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GabDemo.Pattern1.FunctionChaining
{
    public static class HelloWorkflow
    {
        [FunctionName("HelloWorkflow")]
        public static async Task<IEnumerable<string>> Run(
            [OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var output = new List<string>();

            output.Add(await context.CallActivityAsync<string>("SayHello", "GlobalAzureBootcamp 2019"));

            output.Add(await context.CallActivityAsync<string>("SayHello", "Sarajevo"));

            output.Add(await context.CallActivityAsync<string>("SayHello", "Azure fans!"));

            return output;
        }
    }
}