namespace PerfumeShop.Web.ViewModels;

public sealed class CatalogViewModel
{
	public PagedListViewModel<CatalogItemViewModel> PagedList { get; set; }
	public CatalogFilterViewModel CatalogFilter { get; set; }

    public CatalogViewModel(
		PagedListViewModel<CatalogItemViewModel> pagedList,
		CatalogFilterViewModel catalogFilter)
    {
        PagedList = pagedList;
		CatalogFilter = catalogFilter;
    }
}
