namespace PerfumeShop.Core.Models.Entities;

public sealed class CatalogReleaseForm : Entity
{
    public string Name { get; private set; }
    public CatalogReleaseForm(string name)
    {
        Name = name;
    }
}
