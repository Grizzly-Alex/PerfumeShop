namespace PerfumeShop.Core.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetType()
          .GetMember(enumValue.ToString())
          .First()
          .GetCustomAttribute<DisplayAttribute>()
          ?.GetName() ?? string.Empty;
    }

    public static void SeedEnumValues<TEnum, TClass>(this ModelBuilder builder, Func<TEnum, TClass> convert)
        where TClass : class
        where TEnum : Enum
    {
        Enum.GetValues(typeof(TEnum))
            .Cast<object>()
            .Select(value => convert((TEnum)value))
            .ToList()
            .ForEach(instance => builder.Entity<TClass>().HasData(instance));
    }
}
