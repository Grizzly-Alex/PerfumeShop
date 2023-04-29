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

public enum ItemsPerPage
{
    [Display(Name = "10")] Ten = 10,
    [Display(Name = "15")] Twenty = 15,
    [Display(Name = "20")] Threety = 20,
}

public enum OrderStatuses
{
    [Display(Name = "Pending")] Pending = 1,
    [Display(Name = "Approved")] Approved = 2,
    [Display(Name = "InProcess")] InProcess = 3,
    [Display(Name = "Shipped")] Shipped = 4,
    [Display(Name = "Cancelled")] Cancelled = 5,
    [Display(Name = "Refunded")] Refunded = 6,
}

public enum PaymentStatuses
{
    [Display(Name = "Pending")] Pending = 1,
    [Display(Name = "Approved")] Approved = 2,
    [Display(Name = "Reject")] Reject = 3,
}