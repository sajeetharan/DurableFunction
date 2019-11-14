using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;

namespace GabDemo.Pattern5.HumanInteraction.Activities
{
    public static class ApproveNewUserRequest
    {
        [FunctionName("ApproveNewUserRequest")]
        public static async Task Run([ActivityTrigger] DurableActivityContext activityContext)
        {
            // approve user request!
        }
    }
}