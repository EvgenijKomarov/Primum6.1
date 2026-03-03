using Pushables.Abstractions;
using Pushables.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Pushables
{
    public class PublisherService(string url, HttpClient httpClient)
    {
        private PublisherClient client = new PublisherClient(url, httpClient);
        public async Task Push(IPushable message)
        {
            await client.PushEventAsync(message);
        }
    }
}

