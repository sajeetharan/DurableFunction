using Microsoft.Azure.WebJobs;
using System.Threading;
using System.Threading.Tasks;

namespace GabDemo.Pattern4.Monitor.Workflows
{
    public static class OrderMonitor
    {
        [FunctionName("OrderMonitor")]
        public static async Task Run([OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var orderJobId = context.GetInput<string>();

            // poll every two minutes for next two hours
            var expiryTime = context.CurrentUtcDateTime.AddHours(2);

            while (context.CurrentUtcDateTime < expiryTime)
            {
                var isOrderSubmitted = await context.CallActivityAsync<bool>("CheckIfOrderIsSubmitted", orderJobId);

                if (isOrderSubmitted)
                {
                    // order is submitted, let's send the report and exit an orchestrator
                    var report = await context.CallActivityAsync<dynamic>("GenerateReportForManagement", orderJobId);
                    await context.CallActivityAsync("SendReportToManagement", report);
                    return;
                }

                // order is not submitted. Wait for two minutes and check again...
                var nextCheck = context.CurrentUtcDateTime.AddMinutes(2);
                await context.CreateTimer(nextCheck, CancellationToken.None);
            }

            // if order is not submitted in two hours, cancel it
            await context.CallActivityAsync("CancelJob", orderJobId);
        }
    }
}