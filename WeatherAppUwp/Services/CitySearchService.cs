﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherAppUwp.Models;

namespace WeatherAppUwp.Services
{
    /// <summary>
    /// Google city autompletion service
    /// </summary>
    public class CitySearchService
    {
        private const string Url = "https://maps.googleapis.com/maps/api/place/autocomplete/json?input={0}&types=(cities)&language=en_En&key=AIzaSyA0esNcWgLBCPoalqA5T3BxiyqIhhrawJ4";
        /// <summary>
        /// Get list of cities started with the selected query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<List<string>> GetCitiesAsync(string query)
        {
            var uri = new Uri(String.Format(Url, query));
            var client = new HttpClient();

            var json = await client.GetStringAsync(uri);
            var obj = JsonConvert.DeserializeObject<CitiesRoot>(json);
            return obj?.predictions?.Select(x => x.description).ToList();
        }
    }

    
}
