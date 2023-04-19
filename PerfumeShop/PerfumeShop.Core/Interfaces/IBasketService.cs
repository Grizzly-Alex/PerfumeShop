namespace PerfumeShop.Core.Interfaces;

public interface IBasketService
{
    public Task<BasketItem> RemoveItemFromBasketAsync(int basketItemId);
    public Task<Basket> DeleteBasketAsync(int basketId);
    public Task<Basket> AddItemToBasketAsync(int basketId, int productId, int productQuantity = 1);
}
