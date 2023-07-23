namespace PerfumeShop.Core.Interfaces;

public interface IPhysicalShopQueryService
{
    Task<Address> GetAddressAsync(int Id);
}
