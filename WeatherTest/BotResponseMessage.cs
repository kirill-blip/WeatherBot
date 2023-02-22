namespace WeatherTest;

internal class BotResponseMessage
{
    #region Messages
    private string _aboutMessage = "Привет, я Аликс Вэнс. Я могу рассказать тебе про погоду в Самаре.";
    private string _incorectCommandMessage = "Чтобы узнать все мои команды, напишите: \"/commands\"";
    private string _commandsMessage = "/weather - показывает погоду в Самаре\n" +
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
            "/commands" => _commandsMessage,
            "/about" or "/start" => _aboutMessage,
            _ => _incorectCommandMessage
        };
    }
}
