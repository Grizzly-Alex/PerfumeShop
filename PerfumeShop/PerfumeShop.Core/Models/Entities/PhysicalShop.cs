namespace PerfumeShop.Core.Models.Entities;

public sealed class PhysicalShop : Entity
{
    public Address Address { get; private set; }
    public List<DayOfWeek> WeekendOfWeek { get; set; }   
    public TimeOnly StartTimeWork { get; private set; }
    public TimeOnly EndTimeWork { get; private set; }

    public PhysicalShop(Address address, TimeOnly startTimeWork, TimeOnly endTimeWork, List<DayOfWeek> weekendOfWeek)
    {
        Address = Guard.Against.Null(address, nameof(address));
        WeekendOfWeek = Guard.Against.Null(weekendOfWeek, nameof(weekendOfWeek));
        StartTimeWork = Guard.Against.Null(startTimeWork, nameof(startTimeWork));
        EndTimeWork = Guard.Against.Null(endTimeWork, nameof(endTimeWork));
    }
}
