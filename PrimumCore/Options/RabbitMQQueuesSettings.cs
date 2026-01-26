namespace PrimumCore.Options
{
    public class RabbitMQQueuesSettings
    {
        public class QueueDeclaration
        {
            public string Name { get; set; } = string.Empty;
            public bool Durable { get; set; }
            public bool Exclusive { get; set; }
            public bool AutoDelete { get; set; }
        }
    }
}
