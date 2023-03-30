namespace PerfumeShop.Web.Services;

public sealed class ProductViewModelService : ViewModelService<CatalogProduct, CatalogProductViewModel>
{
    public ProductViewModelService(
        IMapper mapper,
        IUnitOfWork<CatalogDbContext> unitOfWork,
        ILogger<ViewModelService<CatalogProduct, CatalogProductViewModel>> logger)
        : base(mapper, unitOfWork, logger)
    {
    }

    public override async Task<IEnumerable<CatalogProductViewModel>> GetViewModelsAsync()
    {
        var models = await _unitOfWork.GetRepository<CatalogProduct>()
            .GetAllAsync(
                include: query => query
                    .Include(product => product.Category)
                    .Include(product => product.Brand)
                    .Include(product => product.Gender)
                    .Include(product => product.Type)
                    .Include(product => product.ReleaseForm),
                isTracking: false);

        if (models is null)
        {
            _logger.LogError("Get_All operation is failed");
            throw new ObjectNotFoundException("Object not found");
        }

        var viewModels = _mapper.Map<IEnumerable<CatalogProductViewModel>>(models);
        return viewModels;
    }

    public override async Task<CatalogProductViewModel> GetViewModelByIdAsync(int id)
    {
        var model = await _unitOfWork.GetRepository<CatalogProduct>()
            .GetFirstOrDefaultAsync(
                predicate: i => i.Id == id,
                include: query => query
                    .Include(product => product.Category)
                    .Include(product => product.Brand)
                    .Include(product => product.Gender)
                    .Include(product => product.Type)
                    .Include(product => product.ReleaseForm),
                isTracking: false);

        if (model is null)
        {
            _logger.LogError("Get_by_id operation is failed");
            throw new ObjectNotFoundException("Object not found");
        }

        var viewModel = _mapper.Map<CatalogProductViewModel>(model);
        return viewModel;
    }

    public override async Task<CatalogProductViewModel> CreateViewModelAsync(CatalogProductViewModel viewModel)
    {
        viewModel.DateDelivery = DateTime.Now;
        var model = _mapper.Map<CatalogProduct>(viewModel);
        _unitOfWork.GetRepository<CatalogProduct>().Add(model);
        await _unitOfWork.SaveChangesAsync();
        return viewModel;
    }

    public override async Task<CatalogProductViewModel> UpdateViewModelAsync(CatalogProductViewModel viewModel)
    {
        viewModel.DateDelivery = await _unitOfWork.GetRepository<CatalogProduct>()
            .GetFirstOrDefaultAsync(
            selector: i => i.DateDelivery,
            predicate: i => i.Id == viewModel.Id);

        var model = _mapper.Map<CatalogProduct>(viewModel);
        _unitOfWork.GetRepository<CatalogProduct>().Update(model);
        await _unitOfWork.SaveChangesAsync();
        return viewModel;
    }
}
