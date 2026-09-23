using System.Collections.Concurrent;
using System.Globalization;
using System.Net;
using System.Web;

namespace CoreDBIteratorTests.Infrastructure
{
    /// <summary>
    /// Заглушка платёжного сервиса и сервисов уведомлений: отвечает по настроенным правилам
    /// и запоминает все запросы, чтобы тесты могли проверить, кого и с какими параметрами вызвали.
    /// </summary>
    public sealed class FakeExternalServices
    {
        private readonly ConcurrentQueue<Uri> _requests = new();

        public ConcurrentDictionary<int, decimal> Balances { get; } = new();
        public ConcurrentDictionary<int, bool> ReadyTeachers { get; } = new();
        public bool PaymentResult { get; set; } = true;
        public HttpStatusCode NotificationStatus { get; set; } = HttpStatusCode.OK;
        /// <summary>Не OK — платёжный сервис «лежит» и отвечает этим кодом на любой запрос</summary>
        public HttpStatusCode PaymentServiceStatus { get; set; } = HttpStatusCode.OK;

        public IReadOnlyList<Uri> Requests => _requests.ToArray();

        public IEnumerable<Uri> PaymentRequests(string pathPrefix) =>
            Requests.Where(r => r.Host == TestEnvironment.PaymentHost && r.AbsolutePath.StartsWith(pathPrefix));

        public HttpMessageHandler CreateHandler() => new Handler(this);

        public HttpClient CreateClient() => new(CreateHandler());

        private HttpResponseMessage Respond(Uri uri)
        {
            _requests.Enqueue(uri);
            var path = uri.AbsolutePath;

            if (uri.Host == TestEnvironment.PaymentHost && PaymentServiceStatus != HttpStatusCode.OK)
                return new HttpResponseMessage(PaymentServiceStatus);
            if (path.StartsWith("/get-student-balance/"))
                return Text(Balances.GetValueOrDefault(IdFrom(path)).ToString(CultureInfo.InvariantCulture));
            if (path.StartsWith("/is-teacher-ready/"))
                return Text(ReadyTeachers.GetValueOrDefault(IdFrom(path)) ? "true" : "false");
            if (path == "/process-lesson-payment")
                return Text(PaymentResult ? "true" : "false");
            if (path == "/publish")
                return new HttpResponseMessage(NotificationStatus);

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        private static int IdFrom(string path) => int.Parse(path[(path.LastIndexOf('/') + 1)..], CultureInfo.InvariantCulture);

        private static HttpResponseMessage Text(string body) => new(HttpStatusCode.OK) { Content = new StringContent(body) };

        private sealed class Handler(FakeExternalServices services) : HttpMessageHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) =>
                Task.FromResult(services.Respond(request.RequestUri!));
        }
    }
}
