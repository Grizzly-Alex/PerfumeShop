namespace PerfumeShop.Core.Models;

public class Role : IdentityRole
{
    public virtual ICollection<UserRole> UserRoles { get; set; }
}
