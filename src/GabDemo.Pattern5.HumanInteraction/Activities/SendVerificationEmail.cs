using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;

namespace GabDemo.Pattern5.HumanInteraction.Activities
{
    public static class SendVerificationEmail
    {
        [FunctionName("SendVerificationEmail")]
        public static async Task Run([ActivityTrigger] DurableActivityContext activityContext)
        {
            // logic for sending verification email...
        }
    }
}