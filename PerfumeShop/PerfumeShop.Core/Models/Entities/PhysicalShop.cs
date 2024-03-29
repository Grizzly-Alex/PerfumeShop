﻿namespace PerfumeShop.Core.Models.Entities;

public sealed class PhysicalShop : Entity
{
    public Address Address { get; private set; }
    public TimeOnly OpenTime { get; private set; }
    public TimeOnly CloseTime { get; private set; }
    public ICollection<DayOfWeek> Weekends { get; private set; }


    public PhysicalShop()
    {
        
    }

    public PhysicalShop(Address address, TimeOnly openTime, TimeOnly closeTime, ICollection<DayOfWeek> weekends)
    {
        Address = Guard.Against.Null(address, nameof(address));
        OpenTime = Guard.Against.Null(openTime, nameof(openTime));
        CloseTime = Guard.Against.Null(closeTime, nameof(closeTime));
        Weekends = weekends;
    }
}
