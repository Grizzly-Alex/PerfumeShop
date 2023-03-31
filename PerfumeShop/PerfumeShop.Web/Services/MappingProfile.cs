namespace PerfumeShop.Web.Services;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CatalogBrand, CatalogItemViewModel>().ReverseMap();
        CreateMap<CatalogCategory, CatalogItemViewModel>().ReverseMap();
        CreateMap<CatalogGender, CatalogItemViewModel>().ReverseMap();
        CreateMap<CatalogReleaseForm, CatalogItemViewModel>().ReverseMap();
        CreateMap<CatalogType, CatalogItemViewModel>().ReverseMap();
        CreateMap<CatalogProduct, CatalogProductViewModel>().ReverseMap()
            .ForMember(model => model.Brand.Name, opt => opt.MapFrom(view => view.Brand))
            .ForMember(model => model.Category.Name, opt => opt.MapFrom(view => view.Category))
            .ForMember(model => model.Type.Name, opt => opt.MapFrom(view => view.Type))
            .ForMember(model => model.Gender.Name, opt => opt.MapFrom(view => view.Gender))
            .ForMember(model => model.ReleaseForm.Name, opt => opt.MapFrom(view => view.ReleaseForm));
    }
}
