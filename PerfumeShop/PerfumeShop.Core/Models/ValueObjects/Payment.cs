namespace PerfumeShop.Core.Models.ValueObjects;

public record Payment(
    Buyer Buyer,
    int OrderId,
    string Currency,
    decimal TotalPrice);