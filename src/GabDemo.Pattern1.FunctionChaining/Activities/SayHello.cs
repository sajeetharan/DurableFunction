using Microsoft.Azure.WebJobs;

namespace GabDemo.Pattern1.FunctionChaining.Activities
{
    public static class SayHello
    {
        [FunctionName("SayHello")]
        public static string Run([ActivityTrigger] DurableActivityContext activityContext)
        {
            var recipient = activityContext.GetInput<string>();

            return $"Hello {recipient}";
        }
    }
}