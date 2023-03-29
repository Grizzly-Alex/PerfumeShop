namespace PerfumeShop.Core.Models.Entities;

public sealed class Category : Entity
{
    public string Name { get; private set; }
    public Category(string name)
    {
        Name = name;
    }
}
