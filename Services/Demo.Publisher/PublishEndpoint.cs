using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.WebPubSub;
using Microsoft.Azure.WebPubSub.Common;
using System.Text.Json;

namespace Demo.Publisher
{
    public static class PublishEndpoint
    {
        [FunctionName("PublishEndpoint")]
        public static async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
                                                         [WebPubSub(Hub = "DemoHub")] IAsyncCollector<WebPubSubAction> actions)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var request = JsonSerializer.Deserialize<ProcessCompletedEventSummary>(requestBody);
            var pce = new ProcessCompletedEvent() { Id = Guid.NewGuid(), ProcessId = request.ProcessId, ClientId = request.ClientId, Timestamp = DateTime.UtcNow };
            var jsonString = JsonSerializer.Serialize(pce);
            //var action = WebPubSubAction.CreateSendToAllAction(jsonString, WebPubSubDataType.Json);

            var actionUser = WebPubSubAction.CreateSendToUserAction(pce.ClientId, jsonString, WebPubSubDataType.Json);
            await actions.AddAsync(actionUser);
            return new OkObjectResult("OK");
        }
    }
}
