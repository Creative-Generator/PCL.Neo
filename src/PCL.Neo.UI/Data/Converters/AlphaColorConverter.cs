using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace PCL.Neo.UI.Data.Converters;

public class AlphaColorConverter : IValueConverter
{
    public static readonly AlphaColorConverter Instance = new();
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Color color && parameter is byte a && targetType.IsAssignableTo(typeof(IBrush)))
        {
            return new SolidColorBrush(new Color(a, color.R, color.G, color.B));
        }
        
        return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}