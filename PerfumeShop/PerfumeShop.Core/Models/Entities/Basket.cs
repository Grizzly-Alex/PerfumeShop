namespace PerfumeShop.Core.Models.Entities;

public sealed class Basket : Entity
{
    private readonly List<BasketItem> _items = new();
    public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();
    public string BuyerId { get; private set; }
    public DateTime CreateDate { get; private set; }
    public DateTime UpdateDate { get; private set; }


    public Basket(string buyerId)
    {
        BuyerId = buyerId;
        CreateDate = DateTime.Now;
        UpdateDate = DateTime.Now;
    }

    public void SetNewBuyerId(string buyerId) => BuyerId = buyerId;


    public void AddItem(int productId, int quantity =1)
    {
        if (!_items.Any(i => i.ProductId == productId))
        {
            _items.Add(new BasketItem(productId, quantity));
        }
        else
        {
            _items.FirstOrDefault(i => i.ProductId == productId)
                !.AddQuantity(quantity);
        }

        UpdateDate = DateTime.Now;
    }

    public void RemoveEmptyItems()
    {
        _items.RemoveAll(i => i.Quantity == 0);

        UpdateDate = DateTime.Now;
    }
}
