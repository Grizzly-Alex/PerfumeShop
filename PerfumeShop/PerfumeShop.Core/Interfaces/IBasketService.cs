namespace PerfumeShop.Core.Interfaces;

public interface IBasketService
{
    Task TransferBasketAsync(string anonymousId, string userName);
    Task<BasketItem> DeleteItemFromBasketAsync(int basketItemId);
    Task<BasketItem> AddItemToBasketAsync(string userName, int productId, int productQuantity = 1);
	Task<BasketItem> UpdateItemBasketAsync(int basketItemId, int productQuantity);
	Task<Basket> ClearBasketAsync(int basketId);
}