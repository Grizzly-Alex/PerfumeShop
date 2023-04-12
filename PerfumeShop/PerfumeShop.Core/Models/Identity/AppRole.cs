namespace PerfumeShop.Core.Models.Identity;

public class AppRole : IdentityRole
{
    public virtual ICollection<AppUserRole> UserRoles { get; set; }
    public virtual ICollection<AppRoleClaim> RoleClaims { get; set; }

    public AppRole() : base() { }

    public AppRole(string roleName) : base(roleName) { } 
}
