namespace PerfumeShop.Core.Interfaces;

public interface IBasketService
{
    Task TransferBasketAsync(string anonymousId, string userName);
    Task<BasketItem> DeleteItemFromBasketAsync(int basketItemId);
    Task<Basket> AddItemToBasketAsync(string userName, int productId, int productQuantity = 1);
    Task<Basket> DeleteBasketAsync(int basketId);
    Task<Basket> GetOrCreateBasketAsync(string userName);
}