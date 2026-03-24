using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Newtonsoft.Json;
using System.Text;
using System.Net;
//using Libs.ConfigConnection;
await MainAsync();

async Task MainAsync()
{


// отключаем проверку сертификатов (для разработки)
ServicePointManager.ServerCertificateValidationCallback +=
    (sender, cert, chain, sslPolicyErrors) => true;

AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);



Console.WriteLine("Starting TelegramBot...");

// ТВОЙ ТОКЕН
string BOT_TOKEN = "8601936183:AAHQ47i1MP-o5bemo7NlFccV_R2GJcsVv7w";

// Получаем URL BotCore из ConfigService
//string botCoreUrl = "https://localhost:5003";
//using var http = new HttpClient();
var handler = new HttpClientHandler()
{
    ServerCertificateCustomValidationCallback =
        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
};

using var http = new HttpClient(handler);

var configJson = await http.GetStringAsync("http://localhost:5000/config");

dynamic config = JsonConvert.DeserializeObject(configJson);

string botCoreUrl = config.BotCore.PublicUrl;

Console.WriteLine($"BotCore URL: {botCoreUrl}");
//string botCoreUrl = ConfigConnection.load_url("BotCore/PublicUrl");
//Console.WriteLine($"BotCore URL: {botCoreUrl}");

var bot = new TelegramBotClient(BOT_TOKEN);

bot.StartReceiving(
    HandleUpdateAsync,
    HandleErrorAsync
);

Console.WriteLine("Telegram bot started");
Console.ReadLine();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    //using var http = new HttpClient();
    var handler = new HttpClientHandler()
{
    ServerCertificateCustomValidationCallback =
        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
};

using var http = new HttpClient(handler);

    if (update.Message != null)
    {
        var chatId = update.Message.Chat.Id;
        var username = update.Message.Chat.Username ?? "unknown";
        var text = update.Message.Text;

        var request = new
        {
            sign = new
            {
                realizationTag = "telegram",
                chatId = chatId,
                username = username
            },
            data = text
        };

        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await http.PostAsync(
            $"{botCoreUrl}/bot-core/text-command",
            content
        );

        var result = await response.Content.ReadAsStringAsync();

        Console.WriteLine("RAW RESPONSE: " + result);

        dynamic obj = JsonConvert.DeserializeObject(result);

        string message = obj.text ?? obj.message ?? "Пустой ответ от сервера";

        //Dictionary<string, string> buttons =
           // obj.buttons.ToObject<Dictionary<string, string>>();

        Dictionary<string, string> buttons = new();
        
        if (obj.buttons !=null)
            {
                buttons = obj.buttons.ToObject<Dictionary<string, string>>();
            }

        if (buttons != null && buttons.Count > 0)
        {
            var keyboard = new InlineKeyboardMarkup(
                buttons.Select(x =>
                    InlineKeyboardButton.WithCallbackData(x.Key, x.Value))
            );

            await botClient.SendTextMessageAsync(
                chatId,
                message,
                replyMarkup: keyboard
            );
        }
        else
        {
            await botClient.SendTextMessageAsync(chatId, message);
        }
    }

    if (update.CallbackQuery != null)
    {
        var chatId = update.CallbackQuery.Message.Chat.Id;
        var username = update.CallbackQuery.Message.Chat.Username ?? "unknown";
        var data = update.CallbackQuery.Data;

        var request = new
        {
            sign = new
            {
                realizationTag = "telegram",
                chatId = chatId,
                username = username
            },
            data = data
        };

        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await http.PostAsync(
            $"{botCoreUrl}/bot-core/callbackquery-command",
            content
        );

        var result = await response.Content.ReadAsStringAsync();

        dynamic obj = JsonConvert.DeserializeObject(result);

        //string message = obj.message;
        string message = obj.text ?? obj.message ?? "Пустой ответ от сервера";

        Dictionary<string, string> buttons = new();
        
        if (obj.buttons !=null)
            {
                buttons = obj.buttons.ToObject<Dictionary<string, string>>();
            }
            //obj.buttons.ToObject<Dictionary<string, string>>();

        if (buttons != null && buttons.Count > 0)
        {
            var keyboard = new InlineKeyboardMarkup(
                buttons.Select(x =>
                    InlineKeyboardButton.WithCallbackData(x.Key, x.Value))
            );

            await botClient.EditMessageTextAsync(
                chatId,
                update.CallbackQuery.Message.MessageId,
                message,
                replyMarkup: keyboard
            );
        }
        else
        {
            await botClient.EditMessageTextAsync(
                chatId,
                update.CallbackQuery.Message.MessageId,
                message
            );
        }
    }
}

Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    Console.WriteLine(exception);
    return Task.CompletedTask;
}
}