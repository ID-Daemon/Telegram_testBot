using System.Data;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

string botToken = "";
var bot = new TelegramBotClient(botToken);
var receiverOptions = new ReceiverOptions
{
    Offset = 0
};
using var cts = new CancellationTokenSource();
bot.StartReceiving(
    Update, 
    Error, 
    receiverOptions,
    cts.Token
    );
Console.WriteLine("Бот вышел в онлайн");
Console.ReadLine();


async static Task Update(ITelegramBotClient bot, Update update, CancellationToken token)
{
    if (update.Message is not null)
    {
        var message = update.Message;
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(message));

        if (message.Text is not null)
        {
            if (message.Text.ToLower().Contains("здорова"))
            {
                await bot.SendTextMessageAsync(message.Chat.Id, "Здоровее видали");
            }
        }
        if (message.Photo is not null)
        {
            await bot.SendTextMessageAsync(message.Chat.Id, "Крутое фото");
        }
    }
}

static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
{
    //throw new NotImplementedException();
    throw new NoNullAllowedException();
}
