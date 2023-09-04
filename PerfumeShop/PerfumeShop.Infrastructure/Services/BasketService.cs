using Microsoft.AspNetCore.Http;

namespace PerfumeShop.Infrastructure.Services;

public class BasketService : IBasketService
{
    private readonly IUnitOfWork<SaleDbContext> _shopping;
    private readonly ILogger<BasketService> _logger;

    public BasketService(
        IUnitOfWork<SaleDbContext> shopping,
        ILogger<BasketService> logger)
    {
        _shopping = shopping;
        _logger = logger;
    }

    public async Task<BasketItem> AddItemToBasketAsync(string userName, int productId, int quantity = 1)
    {
        var basketRepository = _shopping.GetRepository<Basket>();
        var basket = await basketRepository.GetFirstOrDefaultAsync(
            predicate: b => b.BuyerId == userName,
            include: query => query.Include(b => b.Items));

        if (basket is null)
        {
            basket = new Basket(userName);
            basketRepository.Add(basket);
            await _shopping.SaveChangesAsync();
            _logger.LogInformation($"Create basket with ID '{basket.Id}'.");
        }

        BasketItem basketItem = new(productId, quantity);
        basket.AddItem(basketItem);
        basketRepository.Update(basket);
        await _shopping.SaveChangesAsync();
        _logger.LogInformation($"Basket was updated with ID: '{basket.Id}'.");

        return basketItem;
    }

	public async Task<BasketItem> UpdateItemBasketAsync(int basketItemId, int productQuantity)
	{
        var basketItemRepository = _shopping.GetRepository<BasketItem>();
        var basketItem = await basketItemRepository.GetFirstOrDefaultAsync(predicate: i => i.Id == basketItemId)
			?? throw new NullReferenceException($"Basket item not found with ID '{basketItemId}'.");

        if (basketItem.Quantity != productQuantity && productQuantity > 0)
        {
			basketItem.SetQuantity(productQuantity);
			basketItemRepository.Update(basketItem);
			await _shopping.SaveChangesAsync();
			_logger.LogInformation($"Update qty: '{productQuantity}' for basket item with ID: '{basketItemId}'.");
		}		
		return basketItem;
	}

	public async Task<Basket> ClearBasketAsync(int basketId)
    {
        var basketRepository = _shopping.GetRepository<Basket>();
        var basket = await basketRepository.GetFirstOrDefaultAsync(
            predicate: b => b.Id == basketId,
			include: query => query.Include(b => b.Items))
			?? throw new NullReferenceException($"Basket not found with ID '{basketId}'.");

        _shopping.GetRepository<BasketItem>().Remove(basket.Items);
        await _shopping.SaveChangesAsync();
        _logger.LogInformation($"Basket has been cleared with ID: '{basket.Id}'.");

        return basket;
    }

    public async Task<BasketItem> DeleteItemFromBasketAsync(int basketItemId)
    {
        var basketRepository = _shopping.GetRepository<BasketItem>();
        var basketItem = await basketRepository.GetFirstOrDefaultAsync(predicate: b => b.Id == basketItemId)
            ?? throw new NullReferenceException($"Basket item not found with ID '{basketItemId}'.");

        basketRepository.Remove(basketItem);
        await _shopping.SaveChangesAsync();
        _logger.LogInformation($"Basket item was deleted with ID: '{basketItem.Id}'" +
            $" from Basket with ID: '{basketItem.BasketId}'.");

        return basketItem;
    }

    public async Task TransferBasketAsync(string anonymousId, string userName)
    {
        Guard.Against.NullOrEmpty(anonymousId, nameof(anonymousId));
        Guard.Against.NullOrEmpty(userName, nameof(userName));

        var basketRepository = _shopping.GetRepository<Basket>();

        var anonymousBasket = await basketRepository.GetFirstOrDefaultAsync(
            predicate: i => i.BuyerId == anonymousId,
            include: query => query.Include(b => b.Items));

        if (anonymousBasket is not null)
        {
            var userBasket = await basketRepository.GetFirstOrDefaultAsync(
                predicate: i => i.BuyerId == userName,
                include: query => query.Include(b => b.Items));

            if (userBasket is null)
            {
                userBasket = new Basket(userName);
                basketRepository.Add(userBasket);
                await _shopping.SaveChangesAsync();
                _logger.LogInformation($"Create basket with ID '{userBasket.Id}'.");
            }

            userBasket.FillBasket(anonymousBasket.Items);
            basketRepository.Update(userBasket);
            basketRepository.Remove(anonymousBasket);
            await _shopping.SaveChangesAsync();
            _logger.LogInformation($"Basket with ID '{anonymousBasket.Id}' " +
                $"was transferred to Basket with ID '{userBasket.Id}' successful.");
        };
    }
}