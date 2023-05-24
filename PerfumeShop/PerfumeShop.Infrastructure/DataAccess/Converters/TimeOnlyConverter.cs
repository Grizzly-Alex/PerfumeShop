namespace PerfumeShop.Infrastructure.DataAccess.Converters;

public class TimeOnlyConverter : ValueConverter<TimeOnly, TimeSpan>
{
    public TimeOnlyConverter() : base(
        d => d.ToTimeSpan(),
        d => TimeOnly.FromTimeSpan(d))
    { }
}
