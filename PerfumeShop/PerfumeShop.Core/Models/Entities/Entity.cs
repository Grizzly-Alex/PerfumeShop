namespace PerfumeShop.Core.Models.Entities;

public abstract class Entity
{
    [Key]
    public virtual int Id { get; protected set; }
}
