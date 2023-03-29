namespace PerfumeShop.Core.Models.Entities;

public sealed class CatalogBrand : Entity
{
    public string Brand { get; private set; }
    public CatalogBrand(string brand)
    {
        Brand = brand;
    }
}
