namespace PerfumeShop.Web.ViewModels;

public sealed class CatalogViewModel : EntityViewModel
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string? PictureUrl { get; set; }
}
