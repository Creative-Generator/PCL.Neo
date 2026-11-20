using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace PCL.Neo.UI.Data.Converters;

public sealed class AlphaColorConverter(byte alpha) : IValueConverter
{
    public byte Alpha { get; } = alpha;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (!typeof(IBrush).IsAssignableFrom(targetType))
        {
            return new BindingNotification(
                new InvalidCastException($"Target type '{targetType}' is not an IBrush."),
                BindingErrorType.Error);
        }

        var color = value switch
        {
            Color c => c,
            ISolidColorBrush brush => brush.Color,
            _ => (Color?)null
        };

        if (color is not { } result)
        {
            return new BindingNotification(
                new InvalidCastException("Value must be a Color or ISolidColorBrush."),
                BindingErrorType.Error);
        }

        return new Color(Alpha, result.R, result.G, result.B);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}