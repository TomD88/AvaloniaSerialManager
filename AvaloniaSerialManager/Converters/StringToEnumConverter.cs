using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Text;

namespace AvaloniaSerialManager.Converters
{
    //https://stackoverflow.com/questions/555462/cast-with-gettype
    //input -> string
    public class StringToEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null || value == null) return false;
            return value;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null || value == null) return false;
            return System.Convert.ChangeType(value, targetType);
        }
    }
}
