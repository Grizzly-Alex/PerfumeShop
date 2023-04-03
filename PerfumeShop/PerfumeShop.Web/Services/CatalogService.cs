namespace PerfumeShop.Web.Services;

public sealed class CatalogService : ICatalogService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork<CatalogDbContext> _unitOfWork;

    public CatalogService(
        IMapper mapper,
        IUnitOfWork<CatalogDbContext> unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
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
                && (!filter.MinPrice.HasValue || i.Price >= filter.MinPrice)
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

    public async Task<CatalogFilterViewModel> GetCatalogFilterAsync(decimal? minPrice, decimal? maxPrice)
    {
        var brandsFromDb = await _unitOfWork.GetRepository<CatalogBrand>().GetAllAsync();
        var gendersFromDb = await _unitOfWork.GetRepository<CatalogGender>().GetAllAsync();
		var aromaTypesFromDb = await _unitOfWork.GetRepository<CatalogAromaType>().GetAllAsync();
		var releaseFormsFromDb = await _unitOfWork.GetRepository<CatalogReleaseForm>().GetAllAsync();

		var allSelect = new SelectListItem { Text = "All" };

        return new CatalogFilterViewModel(minPrice, maxPrice,
			_mapper.Map<IEnumerable<ItemViewModel>>(brandsFromDb).ToSelectListItems(allSelect),
			_mapper.Map<IEnumerable<ItemViewModel>>(gendersFromDb).ToSelectListItems(allSelect),
			_mapper.Map<IEnumerable<ItemViewModel>>(aromaTypesFromDb).ToSelectListItems(allSelect),
			_mapper.Map<IEnumerable<ItemViewModel>>(releaseFormsFromDb).ToSelectListItems(allSelect));
    }
}
