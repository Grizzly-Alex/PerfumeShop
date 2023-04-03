namespace PerfumeShop.Web.Interfaces;

public interface ICatalogService
{
    Task<decimal?> DefineMaxPrice(decimal? price);
    Task<PagedListViewModel<CatalogItemViewModel>> GetCatalogPagedListAsync(int itemsPerPage, int pageIndex);   
}
