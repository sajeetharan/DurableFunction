using GabDemo.Pattern5.HumanInteraction.Models;
using Microsoft.Azure.WebJobs;
using System;
using System.Threading.Tasks;

namespace GabDemo.Pattern5.HumanInteraction.Workflows
{
    public static class RegisterUserWorkflow
    {
        [FunctionName("RegisterUserWorkflow")]
        public static async Task Run([OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var userData = context.GetInput<User>();

            var userId = await context.CallActivityAsync<long>("CreateNewUser", userData);

            await context.CallActivityAsync("SendVerificationEmail", userId);

            // setting up due time - user has two hours to verify his email, or he needs to request a new verification email
            var dueTime = TimeSpan.FromHours(2);

            try
            {
                // wait for event "UserVerifiedEmail{ID}Event" (e.g. UserVerifiedEmail1Event)
                await context.WaitForExternalEvent($"UserVerifiedEmail{userId}Event", dueTime);

                await context.CallActivityAsync("ApproveNewUserRequest", userId);
            }
            catch (TimeoutException)
            {
                await context.CallActivityAsync("DenyNewUserRequest", userId);
            }
        }
    }
}