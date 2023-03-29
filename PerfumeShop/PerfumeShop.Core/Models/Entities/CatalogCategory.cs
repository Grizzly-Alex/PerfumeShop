namespace PerfumeShop.Core.Models.Entities;

public sealed class CatalogCategory : Entity
{
    public string Category { get; private set; }
    public CatalogCategory(Category category)
    {
        Id = (int)category;

    }
}
