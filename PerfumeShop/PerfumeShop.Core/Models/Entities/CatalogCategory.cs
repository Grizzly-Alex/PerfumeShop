namespace PerfumeShop.Core.Models.Entities;

public sealed class CatalogCategory : Entity
{
    public string Category { get; private set; }
    public CatalogCategory(Category category)
    {
        Id = (int)category;
        Category = category.GetDisplayName();
    }

    private CatalogCategory()
    {      
    }

    public static implicit operator CatalogCategory(Category enumGuitar) => new CatalogCategory(enumGuitar);
    public static implicit operator Category(CatalogCategory classGuitar) => (Category)classGuitar.Id;
}
