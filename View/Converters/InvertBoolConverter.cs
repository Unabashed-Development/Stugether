using System;
using System.Windows.Data;

//class that inverts the bool for xaml binding
//source: https://stackoverflow.com/questions/1039636/how-to-bind-inverse-boolean-properties-in-wpf
//credits to Chris nicol https://stackoverflow.com/users/79910/chris-nicol
namespace View.Converters
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class InvertBooleanConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }

        #endregion
    }
}
