using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using WeatherTest;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var weatherProgram = new WeatherProgram();
        await weatherProgram.StartProgramAsync();
    }
}