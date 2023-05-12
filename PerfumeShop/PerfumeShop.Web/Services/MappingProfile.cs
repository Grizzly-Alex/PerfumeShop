using PerfumeShop.Web.ViewModels.Customer;

namespace PerfumeShop.Web.Services;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Identity       
        CreateMap<AppUser, RegisterUserViewModel>().ReverseMap();
        CreateMap<AppUser, EditUserViewModel>().ReverseMap();        
        CreateMap<AppUser, IndexUserViewModel>().ReverseMap()
            .ForMember(model => model.Email, opt => opt.Ignore())
            .ForMember(model => model.UserName, opt => opt.Ignore());
        CreateMap<UserWithRoleViewModel, AppUser>().ReverseMap()
            .ForMember(view => view.Role, 
                opt => opt.MapFrom(model => model.UserRoles.FirstOrDefault().Role.Name));
        #endregion

        #region Catalog
        CreateMap<CatalogBrand, ItemViewModel>().ReverseMap();
        CreateMap<CatalogGender, ItemViewModel>().ReverseMap();
        CreateMap<CatalogReleaseForm, ItemViewModel>().ReverseMap();
        CreateMap<CatalogAromaType, ItemViewModel>().ReverseMap();
        CreateMap<CatalogProduct, ProductViewModel>().ReverseMap();  
        CreateMap(typeof(PagedList<>), typeof(PagedListViewModel));
        #endregion

        #region Shopping
        CreateMap<Addressee, BuyerViewModel>().ReverseMap();
        CreateMap<AppUser, BuyerViewModel>().ReverseMap();
        CreateMap<Basket, BasketViewModel>()
			.ForMember(model => model.Items, opt => opt.Ignore());        
        #endregion
    }
}
