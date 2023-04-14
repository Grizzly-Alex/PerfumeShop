namespace PerfumeShop.Core.Models.Identity;

public class AppUserLogin : IdentityUserLogin<string>
{
    public virtual AppUser User { get; set; }
}
