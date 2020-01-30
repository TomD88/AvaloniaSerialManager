using Avalonia.Data.Converters;
using System;
using System.Globalization;
using System.IO.Ports;

namespace AvaloniaSerialManager.Converters
{
    public class StringToParityConverter : IValueConverter
    {
        //input -> string
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Enum.GetName(typeof(Parity), value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return (Parity)value;
            else
                return null;
            //return (Parity)Enum.Parse(typeof(Parity), stringValue);
        }
    }
}
