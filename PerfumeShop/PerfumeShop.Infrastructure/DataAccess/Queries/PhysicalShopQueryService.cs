using Address = PerfumeShop.Core.Models.ValueObjects.Address;

namespace PerfumeShop.Infrastructure.DataAccess.Queries;

public class PhysicalShopQueryService : IPhysicalShopQueryService
{
	private readonly IUnitOfWork<SaleDbContext> _unitOfWork;

    public PhysicalShopQueryService(IUnitOfWork<SaleDbContext> unitOfWork)
		=> _unitOfWork = unitOfWork;

    public async Task<IEnumerable<Address>> GetAllAddresses()
	{
		return await _unitOfWork.GetRepository<PhysicalShop>()
			.GetAllAsync(selector: i => i.Address);
	}
}
