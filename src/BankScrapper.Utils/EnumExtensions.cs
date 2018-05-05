using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace BankScrapper.Utils
{
    [DebuggerStepThrough]
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var descriptionAttribute = value.GetAttribute<DescriptionAttribute>();
            if (descriptionAttribute != null)
                return descriptionAttribute.Description;

            return value.ToString();
        }

        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var enumType = value.GetType();

            return enumType
                .GetMember(value.ToString())?
                .FirstOrDefault()?
                .GetCustomAttribute<T>(false);
        }
    }
}