using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WeatherAppUwp.Models;

namespace WeatherAppUwp.Services
{
    public class ForecastService
    {
        private const string Url = "https://query.yahooapis.com/v1/public/yql?format=json&q=select item.condition, location from weather.forecast where woeid in (select woeid from geo.places(1) where text=\"{0}\") and u='c'";
        public async Task<Channel> GetWeather(string place)
        {
            
            var uri = new Uri(String.Format(Url, place));
            var client = new HttpClient();
            
            var json = await client.GetStringAsync(uri);
            var obj = JsonConvert.DeserializeObject<Rootobject>(json);
            return obj?.query?.results?.channel;
           
        }

       
    }
}
