using HnefataflAI.Commons.Positions;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TaflWPF.Utils;
using TaflWPF.ViewModel;

namespace TaflWPF.Converter
{
	class PositionToImageConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
            // get the current border and the outer board grid
            if (values[0] is Border border && values[1] is UniformGrid boardGrid && values[2] is bool showCorners)
			{
                // get the current border index in the board grid
				int index = boardGrid.Children.IndexOf(VisualTreeHelper.GetParent(border) as UIElement);
                // get the board
                BoardViewModel board = ((GameViewModel)boardGrid.DataContext).BoardVM;
                // convert the index to a Position
                Position currentPosition = GridUtils.GetPositionFromIndex(index, board.ColumnCount);
                // check if it's a corner
                if (showCorners && board.Corners.Contains(currentPosition))
                {
                    return new BitmapImage(new Uri(@"pack://application:,,,/Resources/corner.png"));
                }
                // check if it's a throne
                else if (board.Thrones.Contains(currentPosition))
                {
                    return new BitmapImage(new Uri(@"pack://application:,,,/Resources/throne.png"));
                }
            }
            // else, do nothing
			return null;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
