using BotCore.Entities.Inputs;

namespace BotCore.Entities.Outputs
{
    public class Button
    {
        public string Text { get; private set; }
        public string CallbackQuery { get; private set; }

        public Button(string text, CallbackQueryCommand command)
        {
            Text = text;
            CallbackQuery = command.ToString();
        }
    }
}
