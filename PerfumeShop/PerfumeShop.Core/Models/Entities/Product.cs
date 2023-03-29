namespace PerfumeShop.Core.Models.Entities;

public sealed class Product : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string PictureUri { get; private set; }
    public decimal Price { get; private set; }
    public int BrandId { get; private set; }
    public Brand Brand { get; private set; }
    public int GenderId { get; private set; }
    public Gender Gender { get; private set; }
    public int TypeId { get; private set; }
    public Type Type { get; private set; }
    public int ReleaseFormId { get; private set; }
    public ReleaseForm ReleaseForm { get; private set; }
    public int CategoryId { get; private set; }
    public Category Category { get; private set; }

    public Product(
        string name,
        string description,
        decimal price)
    {
        Guard.Against.NullOrEmpty(name, nameof(name));
        Guard.Against.NullOrEmpty(description, nameof(description));
        Guard.Against.NegativeOrZero(price, nameof(price));

        Name = name;
        Description = description;
        Price = price;
    }
}
