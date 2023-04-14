namespace PerfumeShop.Core.Models.Identity;

public class AppUserClaim : IdentityUserClaim<string>
{
    public virtual AppUser User { get; set; }
}
