namespace PerfumeShop.Web.Services;

public class ViewModelService<TModel, TViewModel> : IViewModelService<TModel, TViewModel>
    where TModel : Entity
    where TViewModel : EntityViewModel
{
    protected readonly IMapper _mapper;
    protected readonly IUnitOfWork<CatalogDbContext> _unitOfWork;
    protected readonly ILogger<ViewModelService<TModel, TViewModel>> _logger;

    public ViewModelService(
        IMapper mapper,
        IUnitOfWork<CatalogDbContext> unitOfWork,
        ILogger<ViewModelService<TModel, TViewModel>> logger)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public virtual async Task<TViewModel> CreateViewModelAsync(TViewModel viewModel)
    {
        var model = _mapper.Map<TModel>(viewModel);
        _unitOfWork.GetRepository<TModel>().Add(model);
        await _unitOfWork.SaveChangesAsync();
        return viewModel;
    }

    public virtual async Task<TViewModel> DeleteViewModelAsync(TViewModel viewModel)
    {
        var model = _mapper.Map<TModel>(viewModel);
        _unitOfWork.GetRepository<TModel>().Remove(model);
        await _unitOfWork.SaveChangesAsync();
        return viewModel;
    }

    public virtual async Task<TViewModel> GetViewModelByIdAsync(int id)
    {
        var model = await _unitOfWork.GetRepository<TModel>().GetFirstOrDefaultAsync(
            predicate: i => i.Id == id,
            isTracking: false);

        if (model is null)
        {
            _logger.LogError($"Catalog item with ID: '{id}' not found.");
			throw new NullReferenceException($"Database object with Id: '{id}' has not been founded.");
		}

        var viewModel = _mapper.Map<TViewModel>(model);
        return viewModel;
    }

    public virtual async Task<IEnumerable<TViewModel>> GetViewModelsAsync()
    {
        var models = await _unitOfWork.GetRepository<TModel>().GetAllAsync(isTracking: false);

        if (models is null)
        {
            _logger.LogError("Catalog items not found.");
            throw new NullReferenceException("Objects from database not found.");
        }

        var viewModels = _mapper.Map<IEnumerable<TViewModel>>(models);
        return viewModels;
    }

    public virtual async Task<TViewModel> UpdateViewModelAsync(TViewModel viewModel)
    {
        var model = _mapper.Map<TModel>(viewModel);
        _unitOfWork.GetRepository<TModel>().Update(model);
        await _unitOfWork.SaveChangesAsync();
        return viewModel;
    }
}
