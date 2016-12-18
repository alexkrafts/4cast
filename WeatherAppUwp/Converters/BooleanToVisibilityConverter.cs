using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace WeatherAppUwp.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public bool Inverted { get; set; }
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value is bool && (bool)value & !Inverted) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}