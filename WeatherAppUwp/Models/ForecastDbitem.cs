namespace WeatherAppUwp.Models
{
    public class ForecastDbitem
    {
        public int ForecastDbitemId { get; set; }
        public string Place { get; set; }
        public int Degrees { get; set; }
        public string Condition { get; set; }
        public string ImageUrl { get; set; }
    }
}