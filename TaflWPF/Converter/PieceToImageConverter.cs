using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using TaflWPF.Model.Piece;
using TaflWPF.ViewModel;

namespace TaflWPF.Converter
{
    public class PieceToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
			if (!(value is PieceItemViewModel pvm) || pvm.Piece is null)
				return null;

            switch (pvm.PieceType)
            {
                case PieceType.KING:
                    return new BitmapImage(new Uri(@"pack://application:,,,/Resources/king.png"));
                case PieceType.DEFENDER:
                    return new BitmapImage(new Uri(@"pack://application:,,,/Resources/defender.png"));
                case PieceType.ATTACKER:
                    return new BitmapImage(new Uri(@"pack://application:,,,/Resources/attacker.png"));
                default:
                    return null;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
