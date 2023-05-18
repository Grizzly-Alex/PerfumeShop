namespace PerfumeShop.Web.Utilities;

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
        CreateMap<PaymentCard, PaymentCardViewModel>().ReverseMap();
        CreateMap<Customer, BuyerViewModel>().ReverseMap()
            .ForMember(model => model.UserId, opt => opt.MapFrom(view => view.Id));
        CreateMap<AppUser, BuyerViewModel>().ReverseMap();
        CreateMap<AppUser, AddressViewModel>().ReverseMap();
        CreateMap<Address, AddressViewModel>().ReverseMap();
        CreateMap<Basket, BasketViewModel>()
            .ForMember(model => model.Items, opt => opt.Ignore());
        #endregion
    }
}
