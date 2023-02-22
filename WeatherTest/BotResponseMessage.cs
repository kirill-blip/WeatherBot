namespace WeatherTest;

internal class BotResponseMessage
{
    #region Messages
    private const string AboutMessage = "Привет, я Аликс Вэнс. Я могу рассказать тебе про погоду в Самаре.";
    private const string IncorectCommandMessage = "Чтобы узнать все мои команды, напишите: \"/commands\"";
    private const string CommandsMessage = "/weather - показывает погоду в Самаре\n" +
                "/commands - показывает все команды\n" +
                "/about - рассказывает про себя\n";
    #endregion

    private readonly WeatherProvider _weatherProvider;

    public BotResponseMessage()
    {
        _weatherProvider = new WeatherProvider();
    }

    public async Task<string> GetMessageAsync(string command)
    {
        return command.ToLowerInvariant() switch
        {
            "/weather" => await _weatherProvider.GetWeatherInSamaraAsync(),
            "/commands" => CommandsMessage,
            "/about" or "/start" => AboutMessage,
            _ => IncorectCommandMessage
        };
    }
}
