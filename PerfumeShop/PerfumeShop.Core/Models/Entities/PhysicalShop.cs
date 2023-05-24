namespace PerfumeShop.Core.Models.Entities;

public sealed class PhysicalShop : Entity
{
    public Address Address { get; private set; }
    public TimeOnly OpenTime { get; private set; }
    public TimeOnly CloseTime { get; private set; }
    //public List<DayOfWeek> WeekendOfWeek { get; set; }   

    public PhysicalShop()
    {
        
    }

    public PhysicalShop(Address address, TimeOnly openTime, TimeOnly closeTime)
    {
        Address = Guard.Against.Null(address, nameof(address));
        OpenTime = Guard.Against.Null(openTime, nameof(openTime));
        CloseTime = Guard.Against.Null(closeTime, nameof(closeTime));
        //WeekendOfWeek = Guard.Against.Null(weekendOfWeek, nameof(weekendOfWeek));
    }
}
