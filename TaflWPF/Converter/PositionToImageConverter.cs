using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace TaflWPF.Converter
{
    class PositionToImageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
			if(values[0] is Border border)
			{
				var foo = border.Parent;
			}

            return new BitmapImage(new Uri(@"pack://application:,,,/Resources/board_corner.png"));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
