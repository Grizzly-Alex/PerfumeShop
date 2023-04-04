namespace PerfumeShop.Web.ViewModels;

public sealed class PagedListViewModel : PagedInfoViewModel
{
    public List<CatalogItemViewModel> Items { get; set; } = new();
}
