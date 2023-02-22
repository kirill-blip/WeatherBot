using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;

namespace WeatherTest;

internal class WeatherProgram
{
    private readonly BotResponseMessage _botResponseMessage;

    public WeatherProgram()
    {
        _botResponseMessage = new BotResponseMessage();
    }

    public async Task StartProgramAsync()
    {
        var botClient = new TelegramBotClient("6230158704:AAHkSG4rfuegvyYWO1tqf8ljwWTX-phbjFQ");
        using CancellationTokenSource cts = new();

        var receiverOptions = new ReceiverOptions()
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        botClient.StartReceiving(HandleUpdateAsync, HandlePollingErrorAsync, receiverOptions, cts.Token);
        var me = await botClient.GetMeAsync();

        Console.WriteLine($"Start listening for @{me.Username}");

        Console.ReadLine();

        cts.Cancel();
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is not { } message || message.Text is not { } messageText)
            return;

        var chatId = message.Chat.Id;
        string textMessage = await _botResponseMessage.GetMessageAsync(messageText);

        Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");
        Console.WriteLine($"Bot said: \"{textMessage}\"");

        Message sentMessage = await botClient.SendTextMessageAsync(chatId: chatId, text: textMessage, cancellationToken: cancellationToken);
    }

    private static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
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

}
