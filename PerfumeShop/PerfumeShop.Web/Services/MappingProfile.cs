using PerfumeShop.Core.Models.Identity;

namespace PerfumeShop.Web.Services;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AppUser, RegisterUserViewModel>().ReverseMap();
        CreateMap<AppUser, IndexUserViewModel>().ReverseMap()
            .ForMember(p => p.Email, opt => opt.Ignore())
            .ForMember(p => p.UserName, opt => opt.Ignore());
        CreateMap<CatalogBrand, ItemViewModel>().ReverseMap();
        CreateMap<CatalogGender, ItemViewModel>().ReverseMap();
        CreateMap<CatalogReleaseForm, ItemViewModel>().ReverseMap();
        CreateMap<CatalogAromaType, ItemViewModel>().ReverseMap();
        CreateMap<CatalogProduct, ProductViewModel>().ReverseMap();  
        CreateMap(typeof(PagedList<>), typeof(PagedListViewModel));
	}
}
