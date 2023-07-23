namespace PerfumeShop.Core.Extensions;

public static class IntegerExtensions
{
    public static TEnum? ToEnum<TEnum>(this int value)
		where TEnum : Enum
		=> typeof(TEnum).IsEnumDefined(value) switch
		{
			true => (TEnum)(object)value,
			_ => default,
		};	
}
