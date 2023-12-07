namespace PerfumeShop.Web.Interfaces;

public interface ICatalogViewModelService
{
    Task<decimal?> DefineMaxPrice(decimal? price);
    Task<PagedListViewModel> GetCatalogPagedListAsync(
        int itemsPerPage, int pageIndex,
        decimal? minPrice, decimal? maxPrice,
        int? brandId, int? genderId, int? aromaTypeId, int? releaseFormId);
    Task<CatalogIndexViewModel> GetCatalogIndexAsync(PagedListViewModel pagedList, decimal? minPrice, decimal? maxPrice);
    Task<List<CatalogItemViewModel>> GetAllDiscountedProducts(bool onlyAvailable);
}
