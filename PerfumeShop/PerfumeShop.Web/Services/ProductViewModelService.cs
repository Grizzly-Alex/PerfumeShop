namespace PerfumeShop.Web.Services;

public sealed class ProductViewModelService : ViewModelService<CatalogProduct, ProductViewModel>
{
    public ProductViewModelService(
        IMapper mapper,
        IUnitOfWork<CatalogDbContext> unitOfWork,
        ILogger<ViewModelService<CatalogProduct, ProductViewModel>> logger)
        : base(mapper, unitOfWork, logger)
    {
    }

    public override async Task<IEnumerable<ProductViewModel>> GetViewModelsAsync()
    {
        var models = await _unitOfWork.GetRepository<CatalogProduct>()
            .GetAllAsync(
                include: query => query           
                    .Include(product => product.Brand)
                    .Include(product => product.Gender)
                    .Include(product => product.AromaType)
                    .Include(product => product.ReleaseForm),
                isTracking: false);

        if (models is null)
        {
            _logger.LogError("Get_All operation is failed");
            throw new ObjectNotFoundException("Object not found");
        }

        var viewModels = _mapper.Map<IEnumerable<ProductViewModel>>(models);
        return viewModels;
    }

    public override async Task<ProductViewModel> GetViewModelByIdAsync(int id)
    {
        var model = await _unitOfWork.GetRepository<CatalogProduct>()
            .GetFirstOrDefaultAsync(
                predicate: i => i.Id == id,
                include: query => query
                    .Include(product => product.Brand)
                    .Include(product => product.Gender)
                    .Include(product => product.AromaType)
                    .Include(product => product.ReleaseForm),
                isTracking: false);

        if (model is null)
        {
            _logger.LogError("Get_by_id operation is failed");
            throw new ObjectNotFoundException("Object not found");
        }

        var viewModel = _mapper.Map<ProductViewModel>(model);
        return viewModel;
    }

    public override async Task<ProductViewModel> CreateViewModelAsync(ProductViewModel viewModel)
    {
        viewModel.DateDelivery = DateTime.UtcNow;
        var model = _mapper.Map<CatalogProduct>(viewModel);
        _unitOfWork.GetRepository<CatalogProduct>().Add(model);
        await _unitOfWork.SaveChangesAsync();
        return viewModel;
    }

    public override async Task<ProductViewModel> UpdateViewModelAsync(ProductViewModel viewModel)
    {
        viewModel.DateDelivery = await _unitOfWork.GetRepository<CatalogProduct>()
            .GetFirstOrDefaultAsync(
            selector: i => i.DateDelivery,
            predicate: i => i.Id == viewModel.Id,
            isTracking: false);

        var model = _mapper.Map<CatalogProduct>(viewModel);
        _unitOfWork.GetRepository<CatalogProduct>().Update(model);
        await _unitOfWork.SaveChangesAsync();
        return viewModel;
    }
}
