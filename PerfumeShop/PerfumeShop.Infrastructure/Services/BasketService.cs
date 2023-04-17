namespace PerfumeShop.Infrastructure.Services;

public class BasketService : IBasketService
{
    private const string _basketCookie = "BasketCookie";
    private readonly IUnitOfWork<ShoppingDbContext> _shopping;
    private readonly ILogger<BasketService> _logger;


    public BasketService(
        IUnitOfWork<ShoppingDbContext> shopping,
        ILogger<BasketService> logger)
    {
        _shopping = shopping;
        _logger = logger;
    }

    public async Task<Basket> AddItemToBasketAsync(string userId, int productId, int quantity = 1)
    {
        var basketRepository = _shopping.GetRepository<Basket>();
        var basket = await basketRepository.GetFirstOrDefaultAsync(
            predicate: i => i.BuyerId == userId,
            include: query => query.Include(b => b.Items));

        if (basket is null)
        {
            basket = new Basket(userId);
            basketRepository.Add(basket);

            _logger.LogInformation($"Basket was created for user ID: '{userId}'.");
        }

        basket.AddItem(productId, quantity);

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
        var basketItem = await basketRepository.GetFirstOrDefaultAsync(predicate: b => b.Id == basketItemId)
            ?? throw new NullReferenceException($"Basket item not found with ID '{basketItemId}'.");

        basketRepository.Remove(basketItem);

        _logger.LogInformation($"Basket item was deleted with ID: '{basketItem.Id}' from Basket with ID: '{basketItem.BasketId}'.");

        await _shopping.SaveChangesAsync();
    }
}