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

    public void SetNewBuyerId(string userName) => BuyerId = userName;

    public void AddItem(BasketItem item)
    {
        UpdateDate = DateTime.Now;

        if (!_items.Any(i => i.ProductId == item.ProductId))
        {
            _items.Add(item);
        }
        else
        {
            _items.FirstOrDefault(i => i.ProductId == item.ProductId)
                !.AddQuantity(item.Quantity);
        }     
    }

    public void AddItems(IEnumerable<BasketItem> items)
    {
        foreach (var item in items)
        {
            if (!_items.Any(i => i.ProductId == item.Id))
            {
                _items.Add(new BasketItem(item.Id, item.Quantity));
            }
            else
            {
                _items.FirstOrDefault(i => i.ProductId == item.Id)
                    !.SetQuantity(item.Quantity);
            }
        }
    }

    public void RemoveEmptyItems()
    {
        _items.RemoveAll(i => i.Quantity == 0);

        UpdateDate = DateTime.Now;
    }
}
