using static PrimumCore.Options.RabbitMQQueuesSettings;

namespace PrimumCore.Options
{
    public class RabbitMQSettings
    {
        public bool IsFake { get; set; } = true;
        public string HostName { get; set; } = "localhost";
        public int Port { get; set; } = 5672;
        public string UserName { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public string VirtualHost { get; set; } = "/";
        public List<QueueDeclaration> Queues { get; set; } = new();
    }
}
