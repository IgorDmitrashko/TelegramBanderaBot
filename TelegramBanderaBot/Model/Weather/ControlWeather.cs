using Newtonsoft.Json;
using System.Net;

namespace TelegramBanderaBot.Weather
{
    internal class ControlWeather
    {

        WeatherResponce jsonweatherResponce = new();
        public WeatherResponce GetWeather(string url = "http://api.openweathermap.org/data/2.5/weather?q=Kyiv&appid=883ce8e32c9731721a8c6aeaaee11eb4") {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                string response;
                using(StreamReader stream = new(httpWebResponse.GetResponseStream()))
                {
                    response=stream.ReadToEnd();
                }
                jsonweatherResponce=JsonConvert.DeserializeObject<WeatherResponce>(response);

                jsonweatherResponce.Main.Temp=(int)(jsonweatherResponce.Main.Temp-273.15);
                return jsonweatherResponce;
            }

            catch(Exception)
            {
                return jsonweatherResponce;
            }
        }
    }

    public struct WeatherResponce
    {
        public TemperatureInfo? Main { get; set; }
        public string? Name { get; set; }
        public string? ExceptionWeather { get; set; }
    }
    public class TemperatureInfo
    {
        public float Temp { get; set; }
    }
}
