using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ELECON.Application.Extensions;

public static class EnumExtensions
{
    public static TEnum? GetEnumValueFromDisplayName<TEnum>(string displayName) where TEnum : struct, Enum
    {
        foreach (var field in typeof(TEnum).GetFields())
        {
            var displayAttr = field.GetCustomAttribute<DisplayAttribute>();
            if (displayAttr?.Name == displayName)
            {
                return (TEnum)field.GetValue(null);
            }
        }

        return null; 
    }

    public static bool IsStatusReached<TEnum>(string input) where TEnum : struct, Enum
    {
        return Enum.TryParse<TEnum>(input,ignoreCase: true, out _);
    }
}