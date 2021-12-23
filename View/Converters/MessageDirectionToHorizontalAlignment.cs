using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows;
using Model;

namespace View.Converters
{
    /// <summary>
    /// Converts Model.ChatMessage.MessageDirection to System.Windows.HorizontalAlignment
    /// </summary>
    public class MessageDirectionToHorizontalAlignment : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                throw new InvalidOperationException("Can't convert nothing");
            }
            if (value.GetType() != typeof(ChatMessage.MessageDirection))
            {
                throw new InvalidOperationException("Can only convert Model.ChatMessage.MessageDirection");
            }

            return (ChatMessage.MessageDirection)value switch
            {
                ChatMessage.MessageDirection.Received => HorizontalAlignment.Left,
                ChatMessage.MessageDirection.Send => HorizontalAlignment.Right,
                _ => HorizontalAlignment.Stretch,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
