namespace PerfumeShop.Core.Models.ValueObjects;

public record PaymentCard(
    string Name,
    string CardNumber,
    string ExpirationYear,
    string ExpirationMonth,
    string Cvc);

