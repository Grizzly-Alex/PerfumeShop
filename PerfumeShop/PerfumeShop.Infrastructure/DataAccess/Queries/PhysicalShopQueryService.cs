using Address = PerfumeShop.Core.Models.ValueObjects.Address;
namespace PerfumeShop.Infrastructure.DataAccess.Queries;

public sealed class PhysicalShopQueryService : IPhysicalShopQueryService
{
    private readonly IUnitOfWork<SaleDbContext> _unitOfWork;

    public PhysicalShopQueryService(IUnitOfWork<SaleDbContext> unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<Address> GetAddressAsync(int Id)
        => await _unitOfWork.GetRepository<PhysicalShop>()
            .GetFirstOrDefaultAsync(
            predicate: i => i.Id == Id,
            selector: i => i.Address) 
            ?? throw new NullReferenceException($"Physical Shop not found with ID '{Id}'.");         
}
