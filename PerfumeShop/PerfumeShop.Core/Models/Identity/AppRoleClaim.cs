namespace PerfumeShop.Core.Models.Identity;

public class AppRoleClaim : IdentityRoleClaim<string>
{
    public virtual AppRole Role { get; set; }
}
