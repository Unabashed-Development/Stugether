using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace View.Converters
{
    public class MultiPageEqualsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool ret = true;
            if (values.Length == 0) return true;
            if (values[0].GetType() != typeof(string)) throw new NotImplementedException();
            string first = (string)(values[0]);
            foreach (var val in values)
            {
                if (val.GetType() != typeof(string)) throw new NotImplementedException();
                if (!(
                    ((string)val).Equals(first) ||
                    ((string)val).Equals("View;component/" + first)
                    ))
                    ret = false;
            }
            return ret;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
