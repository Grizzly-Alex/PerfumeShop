using Ardalis.GuardClauses;

namespace PerfumeShop.Infrastructure.Services;

public class BasketService : IBasketService
{
    private const string _basketCookie = "BasketCookie";
    private readonly IUnitOfWork<ShoppingDbContext> _shopping;
    private readonly IUnitOfWork<CatalogDbContext> _catalog;
    private readonly ILogger<BasketService> _logger;


    public BasketService(
        IUnitOfWork<ShoppingDbContext> shopping,
        IUnitOfWork<CatalogDbContext> catalog,
        ILogger<BasketService> logger)
    {
        _shopping = shopping;
        _catalog = catalog;
        _logger = logger;
    }

    public async Task<Basket> AddItemToBasketAsync(string userId, int productId, int productQuantity = 1)
    {
        var basketRepository = _shopping.GetRepository<Basket>();
        var basket = await basketRepository.GetFirstOrDefaultAsync(
            predicate: i => i.BuyerId == userId,
            include: query => query.Include(b => b.Items));

        if (basket is null)
        {
            basket = new Basket(userId);
            basketRepository.Add(basket);
            await _shopping.SaveChangesAsync();

            _logger.LogInformation($"Basket was created for user ID: '{userId}'.");
        }

        var basketItem = await _catalog.GetRepository<CatalogProduct>()
            .GetFirstOrDefaultAsync(
            predicate: product => product.Id == productId,
            selector: product => new BasketItem(product, productQuantity));

        basket.AddItem(basketItem!);

        basketRepository.Update(basket);
        await _shopping.SaveChangesAsync();

        _logger.LogInformation($"Basket was updated with ID: '{basket.Id}'.");

        return basket;
    }

    public async Task DeleteBasketAsync(int basketId)
    {
        var basketRepository = _shopping.GetRepository<Basket>();
        var basket = await basketRepository.GetFirstOrDefaultAsync(predicate: b => b.Id == basketId)
            ?? throw new NullReferenceException($"Basket not found with ID '{basketId}'.");

        basketRepository.Remove(basket);

        _logger.LogInformation($"Basket was deleted with ID: '{basket.Id}'.");

        await _shopping.SaveChangesAsync();
    }

    public async Task RemoveItemFromBasketAsync(int basketItemId)
    {
        var basketRepository = _shopping.GetRepository<BasketItem>();
        var basketItem = await basketRepository
            .GetFirstOrDefaultAsync(predicate: b => b.Id == basketItemId)
            ?? throw new NullReferenceException($"Basket item not found with ID '{basketItemId}'.");

        basketRepository.Remove(basketItem);

        _logger.LogInformation($"Basket item was deleted with ID: '{basketItem.Id}' from Basket with ID: '{basketItem.BasketId}'.");

        await _shopping.SaveChangesAsync();
    }

    public async Task TransferBasketAsync(string anonymousId, string userId)
    {
        Guard.Against.NullOrEmpty(anonymousId, nameof(anonymousId));
        Guard.Against.NullOrEmpty(userId, nameof(userId));

        var basketRepository = _shopping.GetRepository<Basket>();

        var anonymousBasket = await basketRepository.GetFirstOrDefaultAsync(
            predicate: i => i.BuyerId == anonymousId,
            include: query => query.Include(b => b.Items));

        if (anonymousBasket is not null)
        {
            var userBasket = await basketRepository.GetFirstOrDefaultAsync(
                predicate: i => i.BuyerId == userId,
                include: query => query.Include(b => b.Items));

            if (userBasket is null)
            {
                userBasket = new Basket(userId);
                basketRepository.Add(userBasket);
            }

            foreach (var item in anonymousBasket.Items)
            {
                userBasket.AddItem(item);
            }

            basketRepository.Update(userBasket);
            basketRepository.Remove(anonymousBasket);

            await _shopping.SaveChangesAsync();
        };
    }
}
