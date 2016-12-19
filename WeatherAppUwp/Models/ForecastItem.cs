using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Popups;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Template10.Mvvm;
using WeatherAppUwp.Services;

namespace WeatherAppUwp.Models
{
    public class ForecastItem : BindableBase
    {
        public ForecastItem()
        {
        }
       
        private readonly ForecastService _forecastService;
        private bool _loading;
        private string _place;
        private int _degrees;
        private string _condition;
        private bool _error;
        private string _imageUrl;
        private bool _pinned;
        private int _dbId;
        /// <summary>
        /// For a new entries from the view
        /// </summary>
        /// <param name="forecastService"></param>
        /// <param name="query"></param>
        public ForecastItem(ForecastService forecastService, string query)
        {
            _forecastService = forecastService;
            _place = query;
            RequestForecast();
        }
        /// <summary>
        /// For database pulling 
        /// </summary>
        /// <param name="forecastService"></param>
        /// <param name="item">dbSet item</param>
        public ForecastItem(ForecastService forecastService, ForecastDbitem item)
        {
            _error = true;
            _forecastService = forecastService;
            Pinned = true;
            _dbId = item.ForecastDbitemId;
            Place = item.Place;
            Degrees = item.Degrees;
            Condition = item.Condition;
            ImageUrl = item.ImageUrl;
        }

        private async void RequestForecast()
        {
            try
            {
                Loading = true;
                Error = false;
                var channel = await _forecastService.GetWeatherForPlaceAsync(_place);
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


        private void ParseForecast(Channel channel)
        {
            Place = $"{channel.location.city}, {channel.location.country}";
            Degrees = Convert.ToInt32(channel.item.condition.temp);
            Condition = channel.item.condition.text;
            ImageUrl = _forecastService.GetImageUrlFromCode(channel.item.condition.code);
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

        public bool Pinned
        {
            get { return _pinned; }
            set { Set(ref _pinned, value); }
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

        public ICommand PinCommand => new DelegateCommand(Pin);

        public ICommand UnPinCommand => new DelegateCommand(UnPin);

        public void UnPin()
        {
            if (Pinned)
            {
                using (var db = new ForecastContext())
                {
                    db.Database.EnsureCreated();
                    var item = db.Items.First(x => x.ForecastDbitemId == _dbId);
                    if (item != null)
                    {
                        db.Items.Remove(item);
                        db.SaveChanges();
                    }
                }
                Pinned = false;
            }
        }

        private void Pin()
        {
            try
            {
                using (var db = new ForecastContext())
                {
                    db.Database.EnsureCreated();
                    var item = new ForecastDbitem
                    {
                        Place = Place,
                        Degrees = Degrees,
                        Condition = Condition,
                        ImageUrl = ImageUrl
                    };
                    db.Add(item);
                    db.SaveChanges();
                    db.Entry(item).GetDatabaseValues();
                    _dbId = item.ForecastDbitemId;
                }
                Pinned = true;
            }
            catch (Exception exception)
            {
                new MessageDialog(exception.Message).ShowAsync();
            }
        }
    }


}