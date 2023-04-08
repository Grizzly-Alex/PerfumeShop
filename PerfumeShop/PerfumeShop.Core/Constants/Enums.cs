namespace PerfumeShop.Core.Constants;

public enum Gender
{
    [Display(Name = "Unisex")] Unisex = 1,
    [Display(Name = "Man")] Man = 2,
    [Display(Name = "Woman")] Woman = 3,
}

public enum Role
{
    [Display(Name = "Admin")] Admin = 1,
    [Display(Name = "User")] User = 2,
}