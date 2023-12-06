namespace PerfumeShop.Web.ViewModels;

public sealed class CatalogItemViewModel : EntityViewModel
{
    public string Name { get; set; }
    public string Brand { get; set; }   
    public decimal ActualPrice { get; set; }
	public decimal? OldPrice { get; set; }
	public bool IsAvailable { get; set; }
    public string? PictureUri { get; set; }
}
