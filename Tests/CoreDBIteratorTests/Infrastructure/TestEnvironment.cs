namespace CoreDBIteratorTests
{
    /// <summary>
    /// Клиенты платёжки и уведомлений читают адреса из переменных окружения в конструкторе.
    /// Выставляем фиктивные хосты, запросы к ним перехватывает <see cref="Infrastructure.FakeExternalServices"/>.
    /// </summary>
    [SetUpFixture]
    public class TestEnvironment
    {
        public const string PaymentHost = "payment.test";
        public const string ChatBotHost = "chatbot.test";
        public const string MailHost = "mail.test";
        public const string CommonHost = "common.test";

        [OneTimeSetUp]
        public void SetEnvironment()
        {
            Environment.SetEnvironmentVariable("PAYMENTSERVICE_URL", $"http://{PaymentHost}");
            Environment.SetEnvironmentVariable("CHATBOTNOTIFICATIONSERVICE_URL", $"http://{ChatBotHost}");
            Environment.SetEnvironmentVariable("MAILNOTIFICATIONSERVICE_URL", $"http://{MailHost}");
            Environment.SetEnvironmentVariable("COMMONNOTIFICATIONSERVICE_URL", $"http://{CommonHost}");
            // AddCoreContext требует строку подключения; в тестах контекст заменяется на InMemory
            Environment.SetEnvironmentVariable("COREDB_URL", "Host=unused-in-tests");
        }
    }
}
