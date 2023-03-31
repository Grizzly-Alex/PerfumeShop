namespace PerfumeShop.Core.Models.Entities;

public sealed class CatalogType : Entity
{
    public string Name { get; private set; }
    public CatalogType(string name)
    {
        Name = name;
    }
}
