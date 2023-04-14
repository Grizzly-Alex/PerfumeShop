namespace PerfumeShop.Core.Models.Identity;

public class AppUserToken : IdentityUserToken<string>
{
    public virtual AppUser User { get; set; }
}
