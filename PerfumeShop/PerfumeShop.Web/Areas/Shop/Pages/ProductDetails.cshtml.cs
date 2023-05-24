namespace PerfumeShop.Web.Areas.Shop.Pages;

[Area("Shop")]
[AllowAnonymous]
public class ProductDetailsModel : PageModel
{
    public ProductViewModel Product { get; set; } = new();
    private readonly IViewModelService<CatalogProduct, ProductViewModel, CatalogDbContext> _viewModelService;

    public ProductDetailsModel(IViewModelService<CatalogProduct, ProductViewModel, CatalogDbContext> viewModelService)
    {
        _viewModelService = viewModelService;
    }

    public async Task OnGet(int id)
    {
        Product = await _viewModelService.GetViewModelByIdAsync(id);
    }
}
