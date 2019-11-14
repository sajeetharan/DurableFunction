using Microsoft.Azure.WebJobs;
using System;
using System.Threading.Tasks;

namespace GabDemo.Pattern4.Monitor.Workflows
{
    public static class SubmitNewOrder
    {
        [FunctionName("SubmitNewOrder")]
        public static async Task<bool> Run([OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var orderData = context.GetInput<dynamic>();

            await context.CallActivityWithRetryAsync("PayViaMastercard", new RetryOptions(TimeSpan.FromMinutes(3), 3), orderData.MastercardInfo);

            await context.CallActivityAsync("NotifyBuyerOfPayment", orderData.Buyer.ContactEmail);

            await context.CallActivityAsync("NotifySellerOfPayment", orderData.Seller.ContactEmail);

            await context.CallActivityAsync("UpdateOrderInDatabase", orderData);

            await context.CallActivityAsync("ArrangeShipment", orderData.ShipmentDetails);

            return true;
        }
    }
}