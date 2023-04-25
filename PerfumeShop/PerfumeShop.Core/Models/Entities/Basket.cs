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
        if (!_items.Any(i => i.ProductId == item.ProductId))
        {
            _items.Add(item);
        }
        else
        {
            _items.FirstOrDefault(i => i.ProductId == item.ProductId)
                !.AddQuantity(item.Quantity);
        }

		UpdateDate = DateTime.Now;
	}

    public void FillBasket(IEnumerable<BasketItem> items)
    {
        foreach (var item in items)
        {
            if (!_items.Any(i => i.ProductId == item.ProductId))
            {
                _items.Add(new BasketItem(item.ProductId, item.Quantity));
            }
            else
            {
                _items.FirstOrDefault(i => i.ProductId == item.ProductId)
                    !.SetQuantity(item.Quantity);
            }
        }
    }
}
