namespace PerfumeShop.Infrastructure.Services;

public class BasketService : IBasketService
{
    private readonly IUnitOfWork<ShoppingDbContext> _unitOfWork;
    private readonly ILogger<BasketService> _logger;

    public BasketService(
        IUnitOfWork<ShoppingDbContext> unitOfWork,
        ILogger<BasketService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Basket> AddItemToBasketAsync(int basketId, int productId, int quantity = 1)
    {
        var basketRepository = _unitOfWork.GetRepository<Basket>();
        var basket = await basketRepository.GetFirstOrDefaultAsync(
            predicate: i => i.Id == basketId)
            ?? throw new NullReferenceException($"Basket not found with ID '{basketId}'.");

        basket.AddItem(productId, quantity);
        basketRepository.Update(basket);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation($"Basket was updated with ID: '{basket.Id}'.");

        return basket;
    }

    public async Task<Basket> GetOrCreateBasketAsync(string userId)
    {
        var basketRepository = _unitOfWork.GetRepository<Basket>();
        var basket = await basketRepository.GetFirstOrDefaultAsync(predicate: b => b.BuyerId == userId);
        
        if (basket is null)
        {
            basket = new Basket(userId);
            basketRepository.Add(basket);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation($"Create basket for user ID '{userId}'.");
        }
        else
        {
            _logger.LogInformation($"Basket already exists for this user with ID '{userId}'.");
        }        
        return basket;        
    }

    public async Task<Basket> DeleteBasketAsync(int basketId)
    {
        var basketRepository = _unitOfWork.GetRepository<Basket>();
        var basket = await basketRepository.GetFirstOrDefaultAsync(predicate: b => b.Id == basketId)
            ?? throw new NullReferenceException($"Basket not found with ID '{basketId}'.");

        basketRepository.Remove(basket);

        _logger.LogInformation($"Basket was deleted with ID: '{basket.Id}'.");

        await _unitOfWork.SaveChangesAsync();

        return basket;
    }

    public async Task<BasketItem> RemoveItemFromBasketAsync(int basketItemId)
    {
        var basketRepository = _unitOfWork.GetRepository<BasketItem>();
        var basketItem = await basketRepository.GetFirstOrDefaultAsync(predicate: b => b.Id == basketItemId)
            ?? throw new NullReferenceException($"Basket item not found with ID '{basketItemId}'.");

        basketRepository.Remove(basketItem);

        _logger.LogInformation($"Basket item was deleted with ID: '{basketItem.Id}' from Basket with ID: '{basketItem.BasketId}'.");

        await _unitOfWork.SaveChangesAsync();

        return basketItem;
    }
}