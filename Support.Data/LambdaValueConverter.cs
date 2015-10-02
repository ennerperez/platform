using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support
{

    /// <summary>
    /// By Alex Acosta @alexjacostal
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class LambdaValueConverter<TValue> //: System.Windows.Data.IValueConverter
    {

        private Func<TValue, object> lambda;
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            return lambda((TValue)value);

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }



        public LambdaValueConverter(Func<TValue, object> convertfunction)
        {
            lambda = convertfunction;

        }
    }
}
