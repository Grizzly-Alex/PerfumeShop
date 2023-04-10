namespace PerfumeShop.Core.Models;

public class UserRole : IdentityUserRole<string>
{
    public virtual Role Role { get; set; } 
    public virtual AppUser User { get; set; } 
}
