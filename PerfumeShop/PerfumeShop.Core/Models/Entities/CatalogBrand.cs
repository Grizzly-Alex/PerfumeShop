namespace PerfumeShop.Core.Models.Entities;

public sealed class CatalogBrand : Entity
{
    public string Name { get; private set; }

    public CatalogBrand()
    {
        
    }

    public CatalogBrand(string name)
    {
        Name = name;
    }

    public override string ToString() => Name;
}
