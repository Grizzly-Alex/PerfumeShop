namespace PerfumeShop.Web.ViewModels;

public sealed class CatalogItemViewModel : EntityViewModel
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string? PictureUri { get; set; }
}
