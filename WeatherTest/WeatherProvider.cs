using Newtonsoft.Json;
using System.Net;

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

            WeatherInfo weatherInfo = JsonConvert
                .DeserializeObject<WeatherInfo>(
                    await responseMessage.Content.ReadAsStringAsync());

            return "Погода в Самаре:\n" +
               $"Температура: {weatherInfo.temp}\n" +
               $"Ощущается как: {weatherInfo.feels_Like}\n" +
               $"Скорость ветра: {weatherInfo.wind_speed} м\\с";
        }
    }
}
