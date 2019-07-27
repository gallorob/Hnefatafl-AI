using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TaflWPF.Converter
{
	class PositionToImageConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values[0] is Border border)
			{
				if (VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(border)) is UniformGrid uniform)
				{
					var index = uniform.Children.IndexOf(VisualTreeHelper.GetParent(border) as UIElement);

					if (index == 0 || index == uniform.Columns - 1 || index == uniform.Children.Count - uniform.Columns || index == uniform.Children.Count - 1)
						return new BitmapImage(new Uri(@"pack://application:,,,/Resources/board_corner.png"));
				}
			}

			return null;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
