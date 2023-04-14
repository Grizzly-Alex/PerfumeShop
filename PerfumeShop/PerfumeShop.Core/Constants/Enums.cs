namespace PerfumeShop.Core.Constants;

public enum Gender
{
    [Display(Name = "Unisex")] Unisex = 1,
    [Display(Name = "Man")] Man = 2,
    [Display(Name = "Woman")] Woman = 3,
}

public enum Roles
{
    [Display(Name = "Admin")] Admin = 1,
    [Display(Name = "Employee")] Employee = 2,
    [Display(Name = "Customer")] Customer = 3,
}