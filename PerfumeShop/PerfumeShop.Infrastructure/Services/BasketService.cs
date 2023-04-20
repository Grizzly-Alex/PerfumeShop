namespace PerfumeShop.Infrastructure.Services;

public class BasketService : IBasketService
{
    private readonly IUnitOfWork<ShoppingDbContext> _shopping;
    private readonly IUnitOfWork<CatalogDbContext> _catalog;
    private readonly ILogger<BasketService> _logger;

    public BasketService(
        IUnitOfWork<ShoppingDbContext> shopping,
        IUnitOfWork<CatalogDbContext> catalog,
        ILogger<BasketService> logger)
    {
        _catalog = catalog;
        _shopping = shopping;
        _logger = logger;
    }

    public async Task<Basket> AddItemToBasketAsync(string userName, int productId, int quantity = 1)
    {
        var basketRepository = _shopping.GetRepository<Basket>();
        var basket = await basketRepository.GetFirstOrDefaultAsync(
            predicate: i => i.BuyerId == userName,
            include: query => query.Include(b => b.Items));

        if (basket is null)
        {
            basket = new Basket(userName);
            basketRepository.Add(basket);
            await _shopping.SaveChangesAsync();
            _logger.LogInformation($"Create basket with ID '{basket.Id}'.");
        }

        basket.AddItem(productId, quantity);
        basketRepository.Update(basket);
        await _shopping.SaveChangesAsync();
        _logger.LogInformation($"Basket was updated with ID: '{basket.Id}'.");

        return basket;
    }

    public async Task<Basket> GetOrCreateBasketAsync(string userId)
    {
        var basketRepository = _shopping.GetRepository<Basket>();
        var basket = await basketRepository.GetFirstOrDefaultAsync(predicate: b => b.BuyerId == userId);
        
        if (basket is null)
        {
            basket = new Basket(userId);
            basketRepository.Add(basket);
            await _shopping.SaveChangesAsync();
            _logger.LogInformation($"Create basket with ID '{basket.Id}'.");
        }
       
        return basket;        
    }

    public async Task<Basket> DeleteBasketAsync(int basketId)
    {
        var basketRepository = _shopping.GetRepository<Basket>();
        var basket = await basketRepository.GetFirstOrDefaultAsync(predicate: b => b.Id == basketId)
            ?? throw new NullReferenceException($"Basket not found with ID '{basketId}'.");

        basketRepository.Remove(basket);
        await _shopping.SaveChangesAsync();
        _logger.LogInformation($"Basket was deleted with ID: '{basket.Id}'.");

        return basket;
    }

    public async Task<BasketItem> DeleteItemFromBasketAsync(int basketItemId)
    {
        var basketRepository = _shopping.GetRepository<BasketItem>();
        var basketItem = await basketRepository.GetFirstOrDefaultAsync(predicate: b => b.Id == basketItemId)
            ?? throw new NullReferenceException($"Basket item not found with ID '{basketItemId}'.");

        basketRepository.Remove(basketItem);
        await _shopping.SaveChangesAsync();
        _logger.LogInformation($"Basket item was deleted with ID: '{basketItem.Id}' from Basket with ID: '{basketItem.BasketId}'.");

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
                _logger.LogInformation($"Create basket with ID '{userBasket.Id}'.");
            }
            userBasket.AddItems(anonymousBasket.Items);
            basketRepository.Update(userBasket);
            basketRepository.Remove(anonymousBasket);
            await _shopping.SaveChangesAsync();

            _logger.LogInformation($"Basket with ID '{anonymousBasket.Id}' " +
                $"was transferred to Basket with ID '{userBasket.Id}' successful.");
        };
    }
}