namespace PerfumeShop.Core.Models.Entities;

public sealed class CatalogAromaType : Entity
{
    public string Name { get; private set; }
    public CatalogAromaType(string name)
    {
        Name = name;
    }
}
