using Newtonsoft.Json;
using System.Net;

namespace WeatherTest
{
    internal static class Weather
    {
        private static string _url = $"https://api.weather.yandex.ru/v2/fact?lat=53.21326&lon=50.163779&lang=ru_RU";
        private static string _API_KEY = "X-Yandex-API-Key:9c114340-58d8-4836-b72f-9f7afc66b367";

        public static string GetWeatherInSamara()
        {
            WebRequest webRequest = WebRequest.Create(_url);
            webRequest.Headers.Add(_API_KEY);
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
    }
}
