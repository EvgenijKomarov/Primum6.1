using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Pushables
{
    public class PublisherService(string url, HttpClient httpClient)
    {
        private PublisherClient client = new PublisherClient(url, httpClient);
        public async Task Push<TPushable>(TPushable message) where TPushable : IPushable
        {
            var pushableType = typeof(TPushable).Name;
            var json = JsonSerializer.Serialize(message);

            await client.PushAsync(pushableType, json);
        }
    }
}

