using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;

namespace GabDemo.Pattern5.HumanInteraction.Activities
{
    public static class CreateNewUser
    {
        [FunctionName("CreateNewUser")]
        public static async Task<long> Run([ActivityTrigger] DurableActivityContext activityContext)
        {
            // save user data to database...

            // return id

            return 1;
        }
    }
}