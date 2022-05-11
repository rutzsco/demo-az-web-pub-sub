using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.WebPubSub;
using Microsoft.Azure.WebPubSub.Common;

namespace Demo.Publisher
{
    public static class PublishEndpoint
    {
        [FunctionName("PublishEndpoint")]
        public static async Task RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
                                          [WebPubSub(Hub = "DemoHub")] IAsyncCollector<WebPubSubAction> actions)
        {
            await actions.AddAsync(WebPubSubAction.CreateSendToAllAction("Hello Web PubSub!", WebPubSubDataType.Text));
        }
    }
}
