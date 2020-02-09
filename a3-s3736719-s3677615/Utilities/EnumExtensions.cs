using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace a3_s3736719_s3677615.Utilities
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
    }
}
