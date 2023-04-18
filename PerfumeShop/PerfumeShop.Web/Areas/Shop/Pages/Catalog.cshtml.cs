namespace PerfumeShop.Web.Areas.Shop.Pages;

[Area("Shop")]
[AllowAnonymous]
public class CatalogModel : PageModel
{
    public CatalogIndexViewModel CatalogIndex { get; set; } = new();
    private readonly ICatalogViewModelService _catalogService;

    public CatalogModel(ICatalogViewModelService catalogService)
    {
        _catalogService = catalogService;
    }


    public async Task OnGetAsync(CatalogIndexViewModel catalogIndex, PagedInfoViewModel pagedInfo)
    {
        catalogIndex.MaxPrice = await _catalogService.DefineMaxPrice(catalogIndex.MaxPrice);

        var pagedList = await _catalogService.GetCatalogPagedListAsync(
                minPrice: catalogIndex.MinPrice,
                maxPrice: catalogIndex.MaxPrice,
                brandId: catalogIndex.BrandId,
                genderId: catalogIndex.GenderId,
                aromaTypeId: catalogIndex.AromaTypeId,
                releaseFormId: catalogIndex.ReleaseFormId,
                pageIndex: pagedInfo.PageIndex,
                itemsPerPage: pagedInfo.ItemsPerPage == default
                    ? (int)ItemsPerPage.Ten
                    : pagedInfo.ItemsPerPage);

        CatalogIndex = await _catalogService.GetCatalogIndexAsync(
            pagedList, catalogIndex.MinPrice, catalogIndex.MaxPrice);
    }
}
