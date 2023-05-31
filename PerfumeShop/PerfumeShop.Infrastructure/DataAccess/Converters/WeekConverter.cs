namespace PerfumeShop.Infrastructure.DataAccess.Converters;

public class WeekConverter : ValueConverter<List<DayOfWeek>, string>
{
    public WeekConverter() : base(
        w => JsonConvert.SerializeObject(w.Select(d => d.ToString())),
        w => JsonConvert.DeserializeObject<List<DayOfWeek>>(w))
    { }
}