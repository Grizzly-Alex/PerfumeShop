namespace PerfumeShop.Core.Models.Entities;

public sealed class Gender : Entity
{
    public string Name { get; private set; }
    public Gender(string name)
    {
        Name = name;
    }
}
