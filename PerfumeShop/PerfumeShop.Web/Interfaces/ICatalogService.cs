namespace PerfumeShop.Web.Interfaces;

public interface ICatalogService
{
    Task<decimal?> DefineMaxPrice(decimal? price);
    Task<PagedListViewModel<CatalogItemViewModel>> GetCatalogPagedListAsync(CatalogFilterViewModel filter, int itemsPerPage, int pageIndex);
    Task<CatalogFilterViewModel> GetCatalogFilterAsync(decimal maxPrice);
}
