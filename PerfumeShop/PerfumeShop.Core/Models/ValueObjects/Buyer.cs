namespace PerfumeShop.Core.Models.ValueObjects;

public record Buyer(
    string Email,
    string Phone,
    string Name,
    PaymentCard PaymentCard);