namespace PerfumeShop.Core.Models.Entities;

public sealed class CatalogProduct : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string PictureUri { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public int Volume { get; private set; }
    public DateTime DateDelivery { get; private set; }
    public int BrandId { get; private set; }
    public CatalogBrand Brand { get; private set; }
    public int GenderId { get; private set; }
    public CatalogGender Gender { get; private set; }
    public int AromaTypeId { get; private set; }
    public CatalogAromaType AromaType { get; private set; }
    public int ReleaseFormId { get; private set; }
    public CatalogReleaseForm ReleaseForm { get; private set; }
    public int CategoryId { get; private set; }

    public CatalogProduct(string name, string description, decimal price, int volume, int stock)
    {
        Name = Guard.Against.NullOrEmpty(name, nameof(name));
        Description = Guard.Against.NullOrEmpty(description, nameof(description));
        Price = Guard.Against.NegativeOrZero(price, nameof(price));
        Volume = Guard.Against.NegativeOrZero(volume, nameof(volume));
        Stock = Guard.Against.Negative(stock, nameof(stock));
    }
}
