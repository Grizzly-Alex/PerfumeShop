namespace PerfumeShop.Core.Models.Identity;

public class AppUserRole : IdentityUserRole<string>
{
    public virtual IdentityRole<string> Role { get; set; }
}
