namespace PerfumeShop.Web.ViewModels;

public sealed class PagedListViewModel<TModel> : PagedInfoViewModel
{
    public List<TModel> Items { get; set; }
}
