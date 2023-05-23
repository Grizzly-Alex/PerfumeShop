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
            .ForMember(model => model.UserId, opt => opt.MapFrom(view => view.Id))
			.ForMember(model => model.ReceiptEmail, opt => opt.MapFrom(view => view.Email));
		CreateMap<AppUser, BuyerViewModel>().ReverseMap();
        CreateMap<AppUser, AddressViewModel>().ReverseMap();
        CreateMap<Address, AddressViewModel>().ReverseMap();
        CreateMap<Basket, BasketViewModel>()
            .ForMember(model => model.Items, opt => opt.Ignore());
        CreateMap<OrderHeader, OrderViewModel>().ReverseMap()
            .ForMember(model => model.OrderDate, opt => opt.MapFrom(view => view.OrderDate))
            .ForMember(model => model.TrackingId, opt => opt.MapFrom(view => view.TrackingId))
            .ForMember(model => model.OrderStatus.Name, opt => opt.MapFrom(view => view.OrderStatus))
            .ForMember(model => model.PaymentDetail.PaymentStatus.Name, opt => opt.MapFrom(view => view.PaymentStatus))
            .ForMember(model => model.PaymentDetail.PaymentMethod.Name, opt => opt.MapFrom(view => view.PaymentMethod))
            .ForMember(model => model.DeliveryMethod.Name, opt => opt.MapFrom(view => view.DeliveryMethod))
            .ForMember(model => model.Cost.ItemsCost, opt => opt.MapFrom(view => view.ItemsCost))
            .ForMember(model => model.Cost.ShippingCost, opt => opt.MapFrom(view => view.ShippingCost))
            .ForMember(model => model.Cost.PromoCodeCost, opt => opt.MapFrom(view => view.PromoCodeCost))
            .ForMember(model => model.Cost.TotalCost, opt => opt.MapFrom(view => view.TotalPrice))
            .ForMember(model => model.Customer.FirstName, opt => opt.MapFrom(view => view.Buyer.FirstName))
            .ForMember(model => model.Customer.LastName, opt => opt.MapFrom(view => view.Buyer.LastName))
            .ForMember(model => model.Customer.ReceiptEmail, opt => opt.MapFrom(view => view.Buyer.Email))
            .ForMember(model => model.Customer.PhoneNumber, opt => opt.MapFrom(view => view.Buyer.PhoneNumber))
            .ForMember(model => model.ShippingAddress, opt => opt.MapFrom(view => view.ShippingAddress));
        #endregion
    }
}
