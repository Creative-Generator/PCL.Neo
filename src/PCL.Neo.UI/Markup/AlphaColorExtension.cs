using Avalonia.Data;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Media;
using PCL.Neo.UI.Data.Converters;

namespace PCL.Neo.UI.Markup;

public class AlphaColorExtension : MarkupExtension
{
    public AlphaColorExtension()
    {
    }

    public AlphaColorExtension(object color, byte alpha)
    {
        Color = color;
        Alpha = alpha;
    }
    
    public object? Color { get; set; }
    public byte Alpha { get; set; }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Color switch
        {
            Color c => new Color(Alpha, c.R, c.G, c.B),
            BindingBase b => HandleBindingBase(b, serviceProvider),
            _ => throw new NotSupportedException($"Color {Color} is not supported")
        };
    }

    private BindingBase HandleBindingBase(BindingBase binding, IServiceProvider serviceProvider)
    {
        if (binding is ReflectionBinding reflectionBinding)
        {
            reflectionBinding.Converter = new AlphaColorConverter(Alpha);
            return reflectionBinding;
        }

        if (binding is CompiledBinding compiledBinding)
        {
            compiledBinding.Converter = new AlphaColorConverter(Alpha);
            return compiledBinding;
        }

        if (binding is DynamicResourceExtension dynamicResourceExtension)
        {
            var b = dynamicResourceExtension.ProvideValue(serviceProvider);

            return new MultiBinding
            {
                Bindings = { b },
                Converter = new AlphaColorMultiValueConverter(Alpha)
            };
        }

        throw new NotSupportedException($"Binding type {binding.GetType()} is not supported.");
    }
}