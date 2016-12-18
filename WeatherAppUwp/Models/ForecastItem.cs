using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Template10.Mvvm;
using WeatherAppUwp.Services;

namespace WeatherAppUwp.Models
{
    public class ForecastItem : BindableBase
    {
        public ForecastItem()
        {
        }
        public ForecastItem(string place, int degrees, string condition)
        {
            _place = place;
            _degrees = degrees;
            _condition = condition;
        }

        private readonly ForecastService _forecastService;
        private bool _loading;
        private string _place;
        private int _degrees;
        private string _condition;
        private bool _error;
        private string _imageUrl;

        public ForecastItem(ForecastService forecastService, string query)
        {
            _forecastService = forecastService;
            _place = query;
            RequestForecast();
        }

        private async void RequestForecast()
        {
            try
            {
                Loading = true;
                Error = false;
                var channel = await _forecastService.GetWeather(_place);
                if (channel == null)
                    Error = true;
                else
                    ParseForecast(channel);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                Error = true;
            }
            finally
            {
                Loading = false;
            }
        }

        private const string ImageUrlBase = "http://l.yimg.com/a/i/us/we/52/{0}.gif";

        private void ParseForecast(Channel channel)
        {
            Place = $"{channel.location.city}, {channel.location.country}";
            Degrees = Convert.ToInt32(channel.item.condition.temp);
            Condition = channel.item.condition.text;
            ImageUrl = String.Format(ImageUrlBase, channel.item.condition.code);
        }

        public bool Error
        {
            get { return _error; }
            set
            {
                if (value) Condition = "Error";
                Set(ref _error, value);
            }
        }

        public bool Loading
        {
            get { return _loading; }
            set
            {
                if (value) Condition = "Loading";
                Set(ref _loading, value);
            }
        }

        public string Place
        {
            get { return _place; }
            set { Set(ref _place, value); }
        }

        public int Degrees
        {
            get { return _degrees; }
            set { Set(ref _degrees, value); }
        }

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { Set(ref _imageUrl, value); }
        }

        public string Condition
        {
            get { return _condition; }
            set { Set(ref _condition, value); }
        }

        public ICommand RestartCommand => new DelegateCommand(RequestForecast);
    }


}