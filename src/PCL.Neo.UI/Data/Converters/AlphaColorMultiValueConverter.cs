using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace PCL.Neo.UI.Data.Converters;

public sealed class AlphaColorMultiValueConverter(byte alpha) : IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count > 0 && values[0] is Color color)
            return new Color(alpha, color.R, color.G, color.B);

        return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
    }
}