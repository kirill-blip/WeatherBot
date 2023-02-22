using System.Text.Json;

namespace WeatherTest
{
    internal class WeatherProvider
    {
        private const string Url = "https://api.weather.yandex.ru/v2/fact?lat=53.21326&lon=50.163779&lang=ru_RU";
        private const string ApiKey = "9c114340-58d8-4836-b72f-9f7afc66b367";

        private readonly HttpClient _httpClient;

        public WeatherProvider()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("X-Yandex-API-Key", ApiKey);
        }

        public async Task<string> GetWeatherInSamaraAsync()
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(Url);

            JsonSerializerOptions jso = new()
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            };

            WeatherInfo weatherInfo = JsonSerializer.Deserialize<WeatherInfo>(await responseMessage.Content.ReadAsStringAsync(), jso)!;

            return "Погода в Самаре:\n" +
               $"Температура: {weatherInfo.Temp}\n" +
               $"Ощущается как: {weatherInfo.Feels_Like}\n" +
               $"Скорость ветра: {weatherInfo.Wind_Speed} м\\с";
        }
    }
}
