namespace PerfumeShop.Core.Models.Entities;

public sealed class ReleaseForm : Entity
{
    public string Name { get; private set; }
    public ReleaseForm(string name)
    {
        Name = name;
    }
}
