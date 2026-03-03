namespace PublisherService.Entities
{
    public class EmailNotification
    {
        public required string MailAdress { get; set; }
        public required string Title { get; set; }
        public required string Text { get; set; }
    }
}
