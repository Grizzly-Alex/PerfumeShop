namespace PerfumeShop.Core.Interfaces;

public interface IBasketService
{
    public Task TransferBasketAsync(string anonymousId, string userId);
    public Task RemoveItemFromBasketAsync(int basketItemId);
    public Task DeleteBasketAsync(int basketId);
    public Task<Basket> AddItemToBasketAsync(string userId, int productId, int productQuantity = 1);
}
