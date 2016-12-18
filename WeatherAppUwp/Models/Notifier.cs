using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WeatherAppUwp.Models
{
    public class Notifier : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;


        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var handler = PropertyChanged;

            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}