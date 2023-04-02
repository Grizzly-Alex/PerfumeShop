namespace PerfumeShop.Web.Services;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CatalogBrand, CatalogItemViewModel>().ReverseMap();
        CreateMap<CatalogGender, CatalogItemViewModel>().ReverseMap();
        CreateMap<CatalogReleaseForm, CatalogItemViewModel>().ReverseMap();
        CreateMap<CatalogAromaType, CatalogItemViewModel>().ReverseMap();
        CreateMap<CatalogProduct, CatalogProductViewModel>().ReverseMap()
            .ForPath(model => model.Brand.Name, opt => opt.MapFrom(view => view.Brand))
            .ForPath(model => model.AromaType.Name, opt => opt.MapFrom(view => view.AromaType))
            .ForPath(model => model.Gender.Name, opt => opt.MapFrom(view => view.Gender))
            .ForPath(model => model.ReleaseForm.Name, opt => opt.MapFrom(view => view.ReleaseForm));
    }
}
