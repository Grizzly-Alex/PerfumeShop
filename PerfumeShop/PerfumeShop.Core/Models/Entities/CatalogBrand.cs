namespace PerfumeShop.Core.Models.Entities;

public sealed class CatalogBrand : Entity
{
    public string Name { get; private set; }
    public CatalogBrand(string name)
    {
        Name = name;
    }
}
