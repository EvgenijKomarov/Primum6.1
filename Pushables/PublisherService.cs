using System;
using System.Collections.Generic;
using System.Text;

namespace Pushables
{
    public class PublisherService(string url, HttpClient httpClient)
    {
        private PublisherClient client = new PublisherClient(url, httpClient);

    }
}
