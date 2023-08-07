using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaBudget.Models;
public class GlyphPresenter
{
    private GlyphPresenter(object? item) => Item = item;
    public GlyphPresenter(object item, string propertyName):this(item)
    {
        Type itemType = item.GetType();

        PropertyInfo? pi = itemType.GetProperty(propertyName);

        if (pi is not null)
        {
            SetProperty(pi);
        }
        else
        {
            throw new ArgumentException($"Cannot find property for `{propertyName}`", nameof(propertyName));
        }
    }
    public GlyphPresenter(object item, PropertyInfo property) : this(item) => SetProperty(property);

    public PropertyInfo? Property
    {
        get;
        private set;
    }
    
    public char? Glyph => $"{Property?.GetValue(Item)}".ToCharArray().FirstOrDefault();

    public object? Item
    {
        get;
    }

    private void SetProperty(PropertyInfo? property) => Property = property;
}
