namespace PerfumeShop.Core.Models.Entities;

public sealed class BasketItem : Entity
{
    public int BasketId { get; private set; }
    public int ProductId { get; private set; }
    public int Quantity { get; private set; }
    public DateTime CreateDate { get; private set; }

  
    private BasketItem() { }

    public BasketItem(int productId, int quantity)
    {
        ProductId = productId;
        SetQuantity(quantity);
        CreateDate = DateTime.Now;
    }

    public void SetQuantity(int quantity)
    {
        Guard.Against.OutOfRange(quantity, nameof(quantity), 0, int.MaxValue);
        Quantity = quantity;
    }

    public void AddQuantity(int quantity)
    {
        Guard.Against.OutOfRange(quantity, nameof(quantity), 0, int.MaxValue);
        Quantity += quantity;
    }
}