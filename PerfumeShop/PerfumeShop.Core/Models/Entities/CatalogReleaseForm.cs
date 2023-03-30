namespace PerfumeShop.Core.Models.Entities;

public sealed class CatalogReleaseForm : Entity
{
    public string ReleaseForm { get; private set; }
    public CatalogReleaseForm(string releaseForm)
    {
        ReleaseForm = releaseForm;
    }
}
