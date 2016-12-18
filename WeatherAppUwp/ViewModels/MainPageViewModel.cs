using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WeatherAppUwp.Models;
using WeatherAppUwp.Services;

namespace WeatherAppUwp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ObservableCollection<ForecastItem> Items { get; set; }


        private ForecastService _forecastService;
        private CitySearchService _citySearchService;
        public MainPageViewModel()
        {
            _forecastService = new ForecastService();
            _citySearchService = new CitySearchService();
            Items = new ObservableCollection<ForecastItem>();
           
            Items.Add(new ForecastItem(_forecastService, "London"));
        }

        private string _query;
        private List<string> _cities;

        public string Query
        {
            get
            {
                return _query;
            }
            set
            {
                Set(ref _query, value);
                GetPredictions();
            }
        }

        public List<string> Cities
        {
            get { return _cities; }
            set { Set(ref _cities, value); }
        }


        private async void GetPredictions()
        {
            Cities = await _citySearchService.GetCities(_query);
        }

        public ICommand RemoveCommand => new DelegateCommand<ForecastItem>(RemoveItem);

        private void RemoveItem(ForecastItem obj)
        {
            Items.Remove(obj);
        }
        
        
        public void AddCity()
        {
            Items.Add(new ForecastItem(_forecastService, Query));
        }

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoPrivacy() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);

    }
}

