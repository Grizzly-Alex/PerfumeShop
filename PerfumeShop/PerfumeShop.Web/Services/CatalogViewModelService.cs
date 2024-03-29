﻿namespace PerfumeShop.Web.Services;

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
            .MaxAsync(selector: i => i.DiscountPrice == null ? i.Price : i.DiscountPrice);
    }

    public async Task<PagedListViewModel> GetCatalogPagedListAsync(
        int itemsPerPage, int pageIndex,
        decimal? minPrice, decimal? maxPrice,
        int? brandId, int? genderId, int? aromaTypeId, int? releaseFormId, bool onlyDiscount)
    {
        _logger.LogInformation("GetCatalogPagedList called.");

        minPrice ??= (decimal)0.00;

        var pagedList = await _unitOfWork.GetRepository<CatalogProduct>()
            .GetPagedListAsync(
            pageIndex: pageIndex,
            itemsPerPage: itemsPerPage,
            predicate: i => (!minPrice.HasValue || i.DiscountPrice == null ? i.Price >= minPrice : i.DiscountPrice >= minPrice)
                && (!maxPrice.HasValue || i.DiscountPrice == null ? i.Price <= maxPrice : i.DiscountPrice <= maxPrice)
                && (!brandId.HasValue || i.BrandId == brandId)
                && (!genderId.HasValue || i.GenderId == genderId)
                && (!aromaTypeId.HasValue || i.AromaTypeId == aromaTypeId)
                && (!releaseFormId.HasValue || i.ReleaseFormId == releaseFormId)
                && (!onlyDiscount || i.DiscountPrice != null),
            include: i => i.Include(i => i.Brand),
            selector: i => new CatalogItemViewModel
            {
                Id = i.Id,
                Name = i.Name,
                Brand = i.Brand.Name,               
                ActualPrice = i.GetActualPrice(),              
                OldPrice = i.DiscountPrice != null ? i.Price : null,
                IsAvailable = i.Stock > 0,    
                PictureUri = i.PictureUri,
            });

        return _mapper.Map<PagedListViewModel>(pagedList);
    }

    public async Task<CatalogIndexViewModel> GetCatalogIndexAsync(PagedListViewModel pagedList, decimal? minPrice, decimal? maxPrice, bool onlyDiscount)
    {
        _logger.LogInformation("GetCatalogIndex called.");

        var brandsFromDb = await _unitOfWork.GetRepository<CatalogBrand>().GetAllAsync();
        var gendersFromDb = await _unitOfWork.GetRepository<CatalogGender>().GetAllAsync();
        var aromaTypesFromDb = await _unitOfWork.GetRepository<CatalogAromaType>().GetAllAsync();
        var releaseFormsFromDb = await _unitOfWork.GetRepository<CatalogReleaseForm>().GetAllAsync();

        var allSelect = new SelectListItem { Text = "All" };

        return new CatalogIndexViewModel(pagedList, onlyDiscount, minPrice ?? (decimal)0.00, maxPrice,
            brands : _mapper.Map<IEnumerable<ItemViewModel>>(brandsFromDb).ToSelectListItems(allSelect),
            genders :_mapper.Map<IEnumerable<ItemViewModel>>(gendersFromDb).ToSelectListItems(allSelect),
            aromaTypes: _mapper.Map<IEnumerable<ItemViewModel>>(aromaTypesFromDb).ToSelectListItems(allSelect),
            releaseForms: _mapper.Map<IEnumerable<ItemViewModel>>(releaseFormsFromDb).ToSelectListItems(allSelect));
    }

    public async Task<List<CatalogItemViewModel>> GetAllDiscountedProducts(bool onlyAvailable)
    {
        _logger.LogInformation("Get all discounted products.");

        var products = await _unitOfWork.GetRepository<CatalogProduct>()
            .GetAllAsync(
                predicate: onlyAvailable 
                    ? p => p.DiscountPrice != null && p.Stock > 0 
                    : p => p.DiscountPrice != null,
                include: query => query.Include(product => product.Brand),
                isTracking: false);

        return _mapper.Map<List<CatalogItemViewModel>>(products.ToList());
    }
}
