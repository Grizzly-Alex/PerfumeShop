namespace PerfumeShop.Core.Models.Entities;


public sealed class CatalogProduct : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string PictureUri { get; private set; }
    public decimal Price { get; private set; }
    public decimal? DiscountPrice { get; private set; }
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


    public CatalogProduct(
        DateTime dateDelivery, int brandId, int genderId, int aromaTypeId, int releaseFormId,
        string name, decimal price, int stock, int volume, string pictureUri, string description, decimal? discountPrice = null)
    {
		DateDelivery = dateDelivery;
		BrandId = Guard.Against.NegativeOrZero(brandId, nameof(brandId));
        GenderId = Guard.Against.NegativeOrZero(genderId, nameof(genderId));
		AromaTypeId = Guard.Against.NegativeOrZero(aromaTypeId, nameof(aromaTypeId));
		ReleaseFormId = Guard.Against.NegativeOrZero(releaseFormId, nameof(releaseFormId));
		Name = Guard.Against.NullOrEmpty(name, nameof(name));
        Description = Guard.Against.NullOrEmpty(description, nameof(description));
        Price = Guard.Against.NegativeOrZero(price, nameof(price));
        Volume = Guard.Against.NegativeOrZero(volume, nameof(volume));
        Stock = Guard.Against.Negative(stock, nameof(stock));
        PictureUri = Guard.Against.NullOrEmpty(pictureUri, nameof(pictureUri));
        if (discountPrice is not null)
        {
		    DiscountPrice = Guard.Against.Negative((decimal)discountPrice, nameof(discountPrice));		
        }
	}

	public void SetStock(int quantity)
	{
        Stock = Guard.Against.Negative(quantity, nameof(quantity)); ;
	}

    public decimal GetActualPrice() => DiscountPrice == null 
        ? Price 
        : DiscountPrice.Value;
}
