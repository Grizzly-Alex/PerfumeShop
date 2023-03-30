namespace PerfumeShop.Core.Models.Entities;

public sealed class CatalogType : Entity
{
    public string Type { get; private set; }
    public CatalogType(string type)
    {
        Type = type;
    }
}
