namespace PerfumeShop.Web.Services;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CatalogBrand, CatalogBrandViewModel>().ReverseMap();
        CreateMap<CatalogCategory, CatalogCategoryViewModel>().ReverseMap();
        CreateMap<CatalogGender, CatalogGenderViewModel>().ReverseMap();
        CreateMap<CatalogReleaseForm, CatalogReleaseFormViewModel>().ReverseMap();
        CreateMap<CatalogType, CatalogTypeViewModel>().ReverseMap();
        CreateMap<CatalogProduct, CatalogProductViewModel>().ReverseMap();
    }
}
