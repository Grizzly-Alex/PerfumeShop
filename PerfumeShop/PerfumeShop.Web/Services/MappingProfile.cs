namespace PerfumeShop.Web.Services;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CatalogBrand, ItemViewModel>().ReverseMap();
        CreateMap<CatalogGender, ItemViewModel>().ReverseMap();
        CreateMap<CatalogReleaseForm, ItemViewModel>().ReverseMap();
        CreateMap<CatalogAromaType, ItemViewModel>().ReverseMap();
        CreateMap<CatalogProduct, ProductViewModel>().ReverseMap();  
        CreateMap(typeof(PagedList<>), typeof(PagedListViewModel));
	}
}
