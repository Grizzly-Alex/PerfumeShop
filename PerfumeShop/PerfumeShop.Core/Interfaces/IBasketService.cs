namespace PerfumeShop.Core.Interfaces;

public interface IBasketService
{
    Task TransferBasketAsync(string anonymousId, string userName);
    Task<BasketItem> DeleteItemFromBasketAsync(int basketItemId);
    Task<BasketItem> AddItemToBasketAsync(string userName, int productId, int productQuantity = 1);
	Task<BasketItem> UpdateItemBasketAsync(int basketId, int basketItemId, int productQuantity = 1);
	Task<Basket> DeleteBasketAsync(int basketId);
    Task<bool> IsStockQtyAvailable(string userName, int productId, int productQuantity);
    Task<int> GetBasketId(string userName);
}