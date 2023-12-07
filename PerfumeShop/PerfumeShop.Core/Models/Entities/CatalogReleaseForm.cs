namespace PerfumeShop.Core.Models.Entities;

public sealed class CatalogReleaseForm : Entity
{
    public string Name { get; private set; }

    public CatalogReleaseForm()
    {        
    }

    public CatalogReleaseForm(string name)
    {
        Name = name;
    }

    public override string ToString() => Name;
}
