using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif

    public interface IValueConverter
    {
        /// <summary>Converts a value. </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns <see langword="null" />, the valid null value is used.</returns>
        // Token: 0x06001B26 RID: 6950
        object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>Converts a value. </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns <see langword="null" />, the valid null value is used.</returns>
        // Token: 0x06001B27 RID: 6951
        object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }

    public class LambdaValueConverter<TValue> : IValueConverter
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

    //TODO: ValueConverter Implementation (!PROFILE_78)

    #region WIP

#if !PROFILE_78

    public abstract class ValueConverter<TFrom, TTo, TParameter> : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Parameter.ThrowIfNotOfType<TFrom>(value, "value", true);
            Parameter.ThrowIfNotOfType<TParameter>(parameter, "parameter", true);
            if (!targetType.IsAssignableFrom(typeof(TTo)))
                throw new InvalidCastException();
            return ConvertTo((TFrom)((object)value), (TParameter)((object)parameter), culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Parameter.ThrowIfNotOfType<TTo>(value, "value", true);
            Parameter.ThrowIfNotOfType<TParameter>(parameter, "parameter", true);
            if (!targetType.IsAssignableFrom(typeof(TFrom)))
                throw new InvalidCastException();
            return ConvertFrom((TTo)((object)value), (TParameter)((object)parameter), culture);
        }

        protected abstract TTo ConvertTo(TFrom value, TParameter parameter, CultureInfo culture);

        protected virtual TFrom ConvertFrom(TTo value, TParameter parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

#endif

    #endregion WIP

#if PORTABLE
    }

#endif
}