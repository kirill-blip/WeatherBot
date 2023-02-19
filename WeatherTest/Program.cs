using Newtonsoft.Json;
using System.Net;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using WeatherTest;

var botClient = new TelegramBotClient("6230158704:AAHkSG4rfuegvyYWO1tqf8ljwWTX-phbjFQ");

using CancellationTokenSource cts = new();

ReceiverOptions receiverOptions = new()
{
    AllowedUpdates = Array.Empty<UpdateType>()
};

botClient.StartReceiving(HandleUpdateAsync, HandlePollingErrorAsync, receiverOptions, cts.Token);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();
cts.Cancel();


async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    // Only process Message updates: https://core.telegram.org/bots/api#message
    if (update.Message is not { } message)
        return;
    // Only process text messages
    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;
    string textMessage = "Чтобы узнать все мои команды, напишите: \"/commands\"";

    if (messageText.Trim().ToLower() == "weather" || messageText.Trim().ToLower() == "/weather")
    {
        textMessage = GetWeatherInSamara();
    }
    else if (messageText == "/commands")
    {
        textMessage = "/weather - показывает погоду в Самаре\n" +
            "/commands - показывает все команды\n" +
            "/about - рассказывает про себя\n";
    }
    else if (messageText == "/about" || messageText == "/start")
    {
        textMessage = $"Привет, я Алекс. Я могу рассказать тебе про погоду в Самаре.";
    }


    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");
    Console.WriteLine($"Bot said: \"{textMessage}\"");
    // Echo received message text
    Message sentMessage = await botClient.SendTextMessageAsync(
        chatId: chatId,
        text: textMessage,
        cancellationToken: cancellationToken);
}

Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}

string GetWeatherInSamara()
{
    var url = $"https://api.weather.yandex.ru/v2/fact?lat={lat}&lon={lon}&lang=ru_RU";
    WebRequest webRequest = WebRequest.Create(url);
    webRequest.Headers.Add("X-Yandex-API-Key:9c114340-58d8-4836-b72f-9f7afc66b367");
    WebResponse response = webRequest.GetResponse();

    using (Stream stream = response.GetResponseStream())
    {
        using (StreamReader reader = new StreamReader(stream))
        {
            string line = reader.ReadToEnd();

            WeatherInfo weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(line);

            return "Погода в Самаре:\n" +
               $"Температура: {weatherInfo.temp}\n" +
               $"Ощущается как: {weatherInfo.feels_Like}\n" +
               $"Скорость ветра: {weatherInfo.wind_speed} м\\с";
        }
    }
}