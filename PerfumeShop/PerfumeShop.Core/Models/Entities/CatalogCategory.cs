namespace PerfumeShop.Core.Models.Entities;

public sealed class CatalogCategory : Entity
{
    public string Category { get; private set; }
    public CatalogCategory(string category)
    {
        Category = category;
    }
}
