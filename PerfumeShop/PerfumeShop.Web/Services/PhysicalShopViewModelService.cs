namespace PerfumeShop.Web.Services;

public sealed class PhysicalShopViewModelService : ViewModelService<PhysicalShop, ManagePhysicalShopViewModel, SaleDbContext>
{
    public PhysicalShopViewModelService(
        IMapper mapper,
        IUnitOfWork<SaleDbContext> unitOfWork,
        ILogger<ViewModelService<PhysicalShop, ManagePhysicalShopViewModel, SaleDbContext>> logger)
        : base(mapper, unitOfWork, logger)
    {
    }

    public override async Task<PhysicalShop> CreateModelAsync(ManagePhysicalShopViewModel viewModel)
    {
        var model = await base.CreateModelAsync(viewModel);

        return default;
    }

    public override Task<ManagePhysicalShopViewModel> GetViewModelByIdAsync(int id)
    {
        return base.GetViewModelByIdAsync(id);
    }

    public override Task<PhysicalShop> UpdateModelAsync(ManagePhysicalShopViewModel viewModel)
    {
        return base.UpdateModelAsync(viewModel);
    }
}