using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;

namespace GabDemo.Pattern5.HumanInteraction.Activities
{
    public static class DenyNewUserRequest
    {
        [FunctionName("DenyNewUserRequest")]
        public static async Task Run([ActivityTrigger] DurableActivityContext activityContext)
        {
            // deny user request!
        }
    }
}