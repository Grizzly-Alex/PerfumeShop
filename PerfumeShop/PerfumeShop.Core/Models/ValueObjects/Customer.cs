namespace PerfumeShop.Core.Models.ValueObjects;

public sealed class Customer
{
    public string UserId { get; set; }  
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ReceiptEmail { get; set; }
    public string PhoneNumber { get; set; }

    public Customer()
    {       
    }

    public Customer(string userId, string firstName, string lastName, string receiptEmail, string phoneNumber)
    {
        UserId = Guard.Against.NullOrEmpty(userId, nameof(userId));
        FirstName = Guard.Against.Null(firstName, nameof(firstName));
        LastName = Guard.Against.Null(lastName, nameof(lastName));
        ReceiptEmail = Guard.Against.Null(receiptEmail, nameof(receiptEmail));  
        PhoneNumber = Guard.Against.Null(phoneNumber, nameof(phoneNumber));
    }

    public string GetFullName() => string.Concat(FirstName, " ", LastName);
}