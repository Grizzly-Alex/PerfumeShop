namespace PerfumeShop.Web.Services;

public sealed class CatalogService : ICatalogService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork<CatalogDbContext> _unitOfWork;
    private readonly IViewModelService<CatalogBrand, ItemViewModel> _brandService;
    private readonly IViewModelService<CatalogGender, ItemViewModel> _genderService;
    private readonly IViewModelService<CatalogAromaType, ItemViewModel> _aromaTypeService;
    private readonly IViewModelService<CatalogReleaseForm, ItemViewModel> _releaseFormService;

    public CatalogService(
        IMapper mapper,
        IUnitOfWork<CatalogDbContext> unitOfWork,
        IViewModelService<CatalogBrand, ItemViewModel> brandService,
        IViewModelService<CatalogGender, ItemViewModel> genderService,
        IViewModelService<CatalogAromaType, ItemViewModel> typeService,
        IViewModelService<CatalogReleaseForm, ItemViewModel> releaseFormService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _brandService = brandService;
        _genderService = genderService;
        _aromaTypeService = typeService;
        _releaseFormService = releaseFormService;
    }

    public async Task<decimal?> DefineMaxPrice(decimal? price)
    {
        if (price is not null) return price;
        return await _unitOfWork.GetRepository<CatalogProduct>()
            .MaxAsync(selector: i => i.Price);
    }

    public async Task<PagedListViewModel<CatalogItemViewModel>> GetCatalogPagedListAsync(CatalogFilterViewModel filter, int itemsPerPage, int pageIndex)
    {
        var pagedList = await _unitOfWork.GetRepository<CatalogProduct>()
            .GetPagedListAsync(
            pageIndex: pageIndex,
            itemsPerPage: itemsPerPage,
            predicate: i => (filter.MaxPrice.HasValue || i.Price <= filter.MaxPrice)
                && (!filter.BrandId.HasValue || i.BrandId == filter.BrandId)
                && (!filter.GenderId.HasValue || i.GenderId == filter.GenderId)
                && (!filter.AromaTypeId.HasValue || i.AromaTypeId == filter.AromaTypeId)
                && (!filter.ReleaseFormId.HasValue || i.ReleaseFormId == filter.ReleaseFormId),
            selector: i => new CatalogItemViewModel
            {
                Id = i.Id,
                Name = i.Name,
                Price = i.Price,
                PictureUri = i.PictureUri,
            });

        var pagedListView = _mapper.Map<PagedListViewModel<CatalogItemViewModel>>(pagedList);

        return pagedListView;
    }

    public async Task<CatalogFilterViewModel> GetCatalogFilterAsync(decimal maxPrice)
    {
        var brands = await _brandService.GetViewModelsAsync();
        var genders = await _genderService.GetViewModelsAsync();
        var aromaType = await _aromaTypeService.GetViewModelsAsync();
        var releaseForm = await _releaseFormService.GetViewModelsAsync();

        var allSelect = new SelectListItem { Text = "All" };

        return new CatalogFilterViewModel(
            maxPrice,
            brands.ToSelectListItems(allSelect),
            genders.ToSelectListItems(allSelect),
            aromaType.ToSelectListItems(allSelect),
            releaseForm.ToSelectListItems(allSelect));
    }
}
