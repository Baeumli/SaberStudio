using System;
using System.Globalization;
using System.Web;
using System.Windows.Data;

namespace SaberStudio.UI.Converters
{
    public class StringToHtmlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;
            return string.IsNullOrEmpty(str) ? null : HttpUtility.HtmlDecode(str);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;
            return string.IsNullOrEmpty(str) ? null : HttpUtility.HtmlEncode(str);
        }
    }
}