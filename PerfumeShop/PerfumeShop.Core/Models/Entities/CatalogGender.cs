namespace PerfumeShop.Core.Models.Entities;

public sealed class CatalogGender : Entity
{
    public string Gender { get; private set; }
    public CatalogGender(string gender)
    {
        Gender = gender;
    }
}
