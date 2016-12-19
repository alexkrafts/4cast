using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherAppUwp.Models;

namespace WeatherAppUwp.Services
{
    /// <summary>
    /// Yahoo forecast service
    /// </summary>
    public class ForecastService
    {
        public int RetryCount { get; set; } = 5;
        private const string Url = "https://query.yahooapis.com/v1/public/yql?format=json&q=select item.condition, location from weather.forecast where woeid in (select woeid from geo.places(1) where text=\"{0}\") and u='c'";
        private const string ImageUrlBase = "http://l.yimg.com/a/i/us/we/52/{0}.gif";
        /// <summary>
        /// Obtain weather condition for a selected place
        /// </summary>
        /// <param name="place"></param>
        /// <returns></returns>
        public async Task<Channel> GetWeatherForPlaceAsync(string place)
        {
            var uri = new Uri(String.Format(Url, place));
            var client = new HttpClient();

            for (int i = 0; i < RetryCount; i++)
            {
                
                var json = await client.GetStringAsync(uri);
                var obj = JsonConvert.DeserializeObject<Rootobject>(json);
                var result = obj?.query?.results?.channel;
                if (result != null)
                    return result;
            }
            return null;
        }
        /// <summary>
        /// Get image url for a weather condition
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetImageUrlFromCode(string code)
        {
            return String.Format(ImageUrlBase, code);
        }
    }
}
