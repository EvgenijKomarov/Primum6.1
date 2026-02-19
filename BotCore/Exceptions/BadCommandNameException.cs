namespace BotCore.Exceptions
{
    public class BadCommandNameException(string command) : Exception($"Invalid name of command ({command})");
}
