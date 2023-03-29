namespace PerfumeShop.Core.Models.Entities;

public sealed class Brand : Entity
{
    public string Name { get; private set; }
    public Brand(string name)
    {
        Name = name;
    }
}
