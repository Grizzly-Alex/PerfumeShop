namespace PerfumeShop.Web.Services;

public sealed class PhysicalShopViewModelService : ViewModelService<PhysicalShop, PhysicalShopViewModel, SaleDbContext>
{
    public PhysicalShopViewModelService(
        IMapper mapper,
        IUnitOfWork<SaleDbContext> unitOfWork,
        ILogger<ViewModelService<PhysicalShop, PhysicalShopViewModel, SaleDbContext>> logger)
        : base(mapper, unitOfWork, logger)
    {
    }

    public override async Task<PhysicalShop> CreateModelAsync(PhysicalShopViewModel viewModel)
    {
        var model = await base.CreateModelAsync(viewModel);

        return default;
    }

    public override Task<PhysicalShopViewModel> GetViewModelByIdAsync(int id)
    {
        return base.GetViewModelByIdAsync(id);
    }

    public override Task<PhysicalShop> UpdateModelAsync(PhysicalShopViewModel viewModel)
    {
        return base.UpdateModelAsync(viewModel);
    }
}