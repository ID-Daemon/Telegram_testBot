using System.Data;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

string botToken = "";
var bot = new TelegramBotClient(botToken);
var receiverOptions = new ReceiverOptions();
receiverOptions.ThrowPendingUpdates = true;
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

    var message = update.Message;
    var edit = update.EditedMessage;
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(message));
    int sum = 0;
    int mettres = default(int);
    double R = 6371;
    double lat2, lon1, lon2;
    double lat1 = lon1 = 0;
    DateTime editTime1 = DateTime.Now.ToUniversalTime();
    DateTime editTime2;
    if (message is not null)
    {
        if (message!.Text is not null & message!.Text != "??")
        {
            if (message!.Text!.ToLower().Contains("здорова"))
            {
                await bot.SendTextMessageAsync(message.Chat.Id, "Здоровее видали");
            }
        }
        if (message.Photo is not null)
        {
            await bot.SendTextMessageAsync(message.Chat.Id, "Крутое фото");
        }
        if (message.Location is not null)
        {
            lat1 = message.Location.Latitude;
            lon1 = message.Location.Longitude;
            
        }
    }

    if (edit is not null)
    {
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(edit));
            lat2 = edit!.Location!.Latitude;
            lon2 = edit.Location.Longitude;
            editTime2 = edit.EditDate.Value;
            double sin1 = Math.Sin((lat1 - lat2) / 2);
            double sin2 = Math.Sin((lon1 - lon2) / 2);
            sum = Convert.ToInt32(2 * R * Math.Asin(Math.Sqrt(sin1 * sin1 + sin2 * sin2 * Math.Cos(lat1) * Math.Cos(lat2))) / 1000);
        if (mettres is default(int)) mettres = sum;
        else mettres += sum;
            Console.WriteLine($"Пройденное расстояние = {sum} км за {(editTime2-edit.Date).TotalMinutes}  минут");
            lat1 = edit.Location.Latitude;
            lon1 = edit.Location.Longitude;
        editTime1 = editTime2;


    }
}



static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
{
    //throw new NotImplementedException();
    throw new NoNullAllowedException();
}
