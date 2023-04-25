namespace PerfumeShop.Web.Services;

public sealed class CatalogViewModelService : ICatalogViewModelService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork<CatalogDbContext> _unitOfWork;
    private readonly ILogger<CatalogViewModelService> _logger;

    public CatalogViewModelService(
        IMapper mapper,
        IUnitOfWork<CatalogDbContext> unitOfWork,
        ILogger<CatalogViewModelService> logger)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _logger = logger;   
    }

    public async Task<decimal?> DefineMaxPrice(decimal? price)
    {
        _logger.LogInformation("DefineMaxPrice called.");

        if (price is not null) return price;
        return await _unitOfWork.GetRepository<CatalogProduct>()
            .MaxAsync(selector: i => i.Price);
    }

    public async Task<PagedListViewModel> GetCatalogPagedListAsync(
        int itemsPerPage, int pageIndex,
        decimal? minPrice, decimal? maxPrice,
        int? brandId, int? genderId, int? aromaTypeId, int? releaseFormId)
    {
        _logger.LogInformation("GetCatalogPagedList called.");

        minPrice ??= (decimal)0.00;

        var pagedList = await _unitOfWork.GetRepository<CatalogProduct>()
            .GetPagedListAsync(
            pageIndex: pageIndex,
            itemsPerPage: itemsPerPage,
            predicate: i => (!minPrice.HasValue || i.Price >= minPrice)
                && (!maxPrice.HasValue || i.Price <= maxPrice)
                && (!brandId.HasValue || i.BrandId == brandId)
                && (!genderId.HasValue || i.GenderId == genderId)
                && (!aromaTypeId.HasValue || i.AromaTypeId == aromaTypeId)
                && (!releaseFormId.HasValue || i.ReleaseFormId == releaseFormId),
            selector: i => new CatalogItemViewModel
            {
                Id = i.Id,
                Name = i.Name,
                Price = i.Price,
                PictureUri = i.PictureUri,
            });

        return _mapper.Map<PagedListViewModel>(pagedList);
    }

    public async Task<CatalogIndexViewModel> GetCatalogIndexAsync(PagedListViewModel pagedList, decimal? minPrice, decimal? maxPrice)
    {
        _logger.LogInformation("GetCatalogIndex called.");

        var brandsFromDb = await _unitOfWork.GetRepository<CatalogBrand>().GetAllAsync();
        var gendersFromDb = await _unitOfWork.GetRepository<CatalogGender>().GetAllAsync();
        var aromaTypesFromDb = await _unitOfWork.GetRepository<CatalogAromaType>().GetAllAsync();
        var releaseFormsFromDb = await _unitOfWork.GetRepository<CatalogReleaseForm>().GetAllAsync();

        var allSelect = new SelectListItem { Text = "All" };

        return new CatalogIndexViewModel(pagedList, minPrice ?? (decimal)0.00, maxPrice,
            brands : _mapper.Map<IEnumerable<ItemViewModel>>(brandsFromDb).ToSelectListItems(allSelect),
            genders :_mapper.Map<IEnumerable<ItemViewModel>>(gendersFromDb).ToSelectListItems(allSelect),
            aromaTypes: _mapper.Map<IEnumerable<ItemViewModel>>(aromaTypesFromDb).ToSelectListItems(allSelect),
            releaseForms: _mapper.Map<IEnumerable<ItemViewModel>>(releaseFormsFromDb).ToSelectListItems(allSelect));
    }
}
