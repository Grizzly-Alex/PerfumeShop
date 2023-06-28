using PerfumeShop.Web.ViewModels.Order;

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

        #region Sale
        CreateMap<PaymentCard, PaymentCardViewModel>().ReverseMap();

        CreateMap<Customer, BuyerViewModel>().ReverseMap()
            .ForMember(model => model.UserId, opt => opt.MapFrom(view => view.Id))
			.ForMember(model => model.ReceiptEmail, opt => opt.MapFrom(view => view.Email));

		CreateMap<AppUser, BuyerViewModel>().ReverseMap();

        CreateMap<AppUser, AddressViewModel>().ReverseMap();

        CreateMap<Address, AddressViewModel>().ReverseMap();

        CreateMap<Basket, BasketViewModel>()
            .ForMember(model => model.Items, opt => opt.Ignore());

        CreateMap<OrderInfoViewModel, OrderHeader>().ReverseMap()
            .ForMember(view => view.OrderStatus, opt => opt.MapFrom(model => model.OrderStatus.Name))
            .ForMember(view => view.PaymentStatus, opt => opt.MapFrom(model => model.PaymentDetail.PaymentStatus.Name))
            .ForMember(view => view.PaymentMethod, opt => opt.MapFrom(model => model.PaymentDetail.PaymentMethod.Name))
            .ForMember(view => view.DeliveryMethod, opt => opt.MapFrom(model => model.DeliveryDetail.DeliveryMethod.Name))
            .ForMember(view => view.ItemsCost, opt => opt.MapFrom(model => model.Cost.ItemsCost))
            .ForMember(view => view.ShippingCost, opt => opt.MapFrom(model => model.Cost.ShippingCost))
            .ForMember(view => view.PromoCodeCost, opt => opt.MapFrom(model => model.Cost.PromoCodeCost))
            .ForMember(view => view.TotalPrice, opt => opt.MapFrom(model => model.Cost.TotalCost))
            .ForMember(view => view.CustomerName, opt => opt.MapFrom(model => model.Customer.GetFullName()))
            .ForMember(view => view.CustomerPhone, opt => opt.MapFrom(model => model.Customer.PhoneNumber))
            .ForMember(view => view.CustomerEmail, opt => opt.MapFrom(model => model.Customer.ReceiptEmail))
            .ForMember(view => view.Address, opt => opt.MapFrom(model => model.DeliveryDetail.DeliveryAddress.GetFullAddress()));

        CreateMap<PhysicalShopViewModel, PhysicalShop>().ReverseMap();

        CreateMap<PaymentMethod, ItemViewModel>().ReverseMap();

        #endregion
    }
}
