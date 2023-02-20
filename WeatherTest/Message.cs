namespace WeatherTest
{
    internal class Message
    {
        #region messages
        private string _aboutMessage = "Привет, я Аликс Вэнс. Я могу рассказать тебе про погоду в Самаре.";
        private string _incorectCommandMessage = "Чтобы узнать все мои команды, напишите: \"/commands\"";
        private string _commandsMessage = "/weather - показывает погоду в Самаре\n" +
                    "/commands - показывает все команды\n" +
                    "/about - рассказывает про себя\n";
        #endregion

        public virtual string GetMessage(string command)
        {
            switch (command.ToLower())
            {
                case "/weather":
                    return Weather.GetWeatherInSamara();
                case "/commands":
                    return _commandsMessage;
                case "/about":
                case "/start":
                    return _aboutMessage;
                default:
                    return _incorectCommandMessage;
            }
        }
    }
}
