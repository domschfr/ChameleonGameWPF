using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ChameleonGame.View
{
    public class ImageSourceConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string filename && !string.IsNullOrEmpty(filename))
            {
                try
                {
                    return new BitmapImage(new Uri($"pack://application:,,,/Images/{filename}"));
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }
        public object? ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
