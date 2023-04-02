namespace PerfumeShop.Web.Services;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CatalogBrand, CatalogItemViewModel>().ReverseMap();
        CreateMap<CatalogGender, CatalogItemViewModel>().ReverseMap();
        CreateMap<CatalogReleaseForm, CatalogItemViewModel>().ReverseMap();
        CreateMap<CatalogAromaType, CatalogItemViewModel>().ReverseMap();
        CreateMap<CatalogProduct, CatalogProductViewModel>().ReverseMap();           
	}
}
