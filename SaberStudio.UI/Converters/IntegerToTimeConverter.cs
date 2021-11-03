using System;
using System.Globalization;
using System.Windows.Data;

namespace SaberStudio.UI.Converters
{
    public class IntegerToTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException();

            var timeInSeconds = (int) value;
            
            if (timeInSeconds == 0)
                return "-";

            var timeSpan = TimeSpan.FromSeconds(timeInSeconds);
            
            return timeSpan.ToString(@"m\:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
