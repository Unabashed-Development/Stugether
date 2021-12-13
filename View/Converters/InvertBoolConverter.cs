using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace View.Converters
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class InvertBooleanConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {

            /*            if (targetType != typeof(Boolean))
                        {
                            throw new InvalidOperationException("The target must be a boolean");
                        }*/

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            /*return (bool)value ? !(bool)value : Binding.DoNothing;*/
            return !(bool)value;
        }

        #endregion
    }
}
