namespace PerfumeShop.Core.Interfaces;

public interface IPhysicalShopQueryService
{
    Task<Address> GetAddress(int Id);
}
