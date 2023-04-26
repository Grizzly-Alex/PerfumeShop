namespace PerfumeShop.Web.Interfaces;

public interface IBasketViewModelService
{
    Task<BasketViewModel> GetBasketForUserAsync(string userName);
}