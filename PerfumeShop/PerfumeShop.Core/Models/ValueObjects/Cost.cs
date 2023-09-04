namespace PerfumeShop.Core.Models.ValueObjects;

public sealed class Cost
{
    public decimal ItemsCost { get; private set; }
    public decimal TotalCost { get; private set; }

    public Cost(decimal itemsCost, decimal totalCost)
    {
        ItemsCost = Guard.Against.NegativeOrZero(itemsCost, nameof(itemsCost)); 
        TotalCost = Guard.Against.NegativeOrZero(totalCost, nameof(totalCost));
    }
}
