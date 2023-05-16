namespace PerfumeShop.Core.Models.ValueObjects;

public record Buyer(
    string Email,
    string Name,
    PaymentCard PaymentCard);