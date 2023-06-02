namespace PerfumeShop.Core.Interfaces;

public interface IPhysicalShopQueryService
{
	Task<IEnumerable<Address>> GetAllAddresses();
}
