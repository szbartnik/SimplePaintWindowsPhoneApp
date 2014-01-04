using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace DrawOnMe.Converters
{
    public class ContrastColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                SolidColorBrush brush = new SolidColorBrush((value as SolidColorBrush).Color);
                brush.Color = InvertMeAColour(brush.Color);
                return brush;
            }
            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        Color InvertMeAColour(Color ColourToInvert)
        {
            return Color.FromArgb(ColourToInvert.A, (byte)~ColourToInvert.R, (byte)~ColourToInvert.G, (byte)~ColourToInvert.B);
        }
    }
}
