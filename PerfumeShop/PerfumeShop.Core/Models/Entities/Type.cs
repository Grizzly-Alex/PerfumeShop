namespace PerfumeShop.Core.Models.Entities;

public sealed class Type : Entity
{
    public string Name { get; private set; }
    public Type(string name)
    {
        Name = name;
    }
}
