namespace PerfumeShop.Core.Models.ValueObjects;

public sealed class Cost
{
    public decimal ItemsCost { get; private set; }
    public decimal ShippingCost { get; private set; }
    public decimal PromoCodeCost { get; private set; }
    public decimal TotalCost { get; private set; }

    public Cost(decimal itemsCost, decimal shippingCost, decimal promoCodeCost, decimal totalCost)
    {
        ItemsCost = Guard.Against.NegativeOrZero(itemsCost, nameof(itemsCost)); ;
        ShippingCost = Guard.Against.Negative(shippingCost, nameof(shippingCost));
        PromoCodeCost = Guard.Against.Negative(promoCodeCost, nameof(promoCodeCost));
        TotalCost = Guard.Against.NegativeOrZero(totalCost, nameof(totalCost));
    }
}
