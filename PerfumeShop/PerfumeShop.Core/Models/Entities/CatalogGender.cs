namespace PerfumeShop.Core.Models.Entities;

public sealed class CatalogGender : Entity
{
    public string Name { get; private set; }
    public CatalogGender(string name)
    {
        Name = name;
    }
}
