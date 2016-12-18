using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAppUwp.Models
{
    public class CitiesRoot
    {
        public Predictions[] predictions { get; set; }
    }

    public class Predictions
    {
        public string description { get; set; }
    }
}
