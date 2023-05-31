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

public enum Month
{
    [Display(Name = "1")] January = 1,
    [Display(Name = "2")] February = 2,
    [Display(Name = "3")] March = 3,
    [Display(Name = "4")] April = 4,
    [Display(Name = "5")] May = 5,
    [Display(Name = "6")] June = 6,
    [Display(Name = "7")] July = 7,
    [Display(Name = "8")] August = 8,
    [Display(Name = "9")] September = 9,
    [Display(Name = "10")] October = 10,
    [Display(Name = "11")] November = 11,
    [Display(Name = "12")] December = 12,
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

public enum PaymentMethods
{
	[Display(Name = "Payment Place")] PaymentPlace = 1,
	[Display(Name = "Payment Remote")] PaymentRemote = 2,
}

public enum OrderReceiptMethods
{
	[Display(Name = "Pickup")] Pickup = 1,
	[Display(Name = "Courier")] Courier = 2,
}