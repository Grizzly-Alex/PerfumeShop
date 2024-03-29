﻿namespace PerfumeShop.Web.ViewModels;

public sealed class CatalogIndexViewModel
{
    public PagedListViewModel PagedList { get; set; }

    public int? BrandId { get; set; }
    public int? GenderId { get; set; }
    public int? AromaTypeId { get; set; }
    public int? ReleaseFormId { get; set; }

    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    [Display(Name = "Only Discount")]
    public bool OnlyDiscount { get; set; }

    [Display(Name = "Brand")]
    public IEnumerable<SelectListItem>? Brands { get; set; }

    [Display(Name = "Gender")]
    public IEnumerable<SelectListItem>? Genders { get; set; }

    [Display(Name = "Aroma Type")]
    public IEnumerable<SelectListItem>? AromaTypes { get; set; }

    [Display(Name = "Release Form")]
    public IEnumerable<SelectListItem>? ReleaseForms { get; set; }

    public CatalogIndexViewModel()
    {
        PagedList = new PagedListViewModel();
    }

    public CatalogIndexViewModel(
        PagedListViewModel pagedList,
        bool onlyDiscount,
        decimal? minPrice,
        decimal? maxPrice,
        IEnumerable<SelectListItem>? brands,
        IEnumerable<SelectListItem>? genders,
        IEnumerable<SelectListItem>? aromaTypes,
        IEnumerable<SelectListItem>? releaseForms)
    {
        PagedList = pagedList;
        OnlyDiscount = onlyDiscount;
        MinPrice = minPrice;
        MaxPrice = maxPrice;
        Brands = brands;
        Genders = genders;
        AromaTypes = aromaTypes;
        ReleaseForms = releaseForms;
    }
}
