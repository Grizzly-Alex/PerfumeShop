namespace PerfumeShop.Infrastructure.DataAccess.Converters;

public class EnumConverter<TEnum> : ValueConverter<ICollection<TEnum>, string>
    where TEnum : Enum
{
    public EnumConverter() : base(
        e => JsonConvert.SerializeObject(e.Select(e => e.ToString() ?? string.Empty)),
        e => JsonConvert.DeserializeObject<ICollection<TEnum>>(e))
    { }
}