namespace PerfumeShop.Core.Models;

public sealed class AppUser : IdentityUser
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? StreetAddress { get; private set; }
    public string? City { get; private set; }
    public string? State { get; private set; }
    public string? PostalCode { get; private set; }
}
