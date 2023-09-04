namespace PerfumeShop.Core.Models.ValueObjects;

public record PaymentCard(
    string NameOwner,
    string CardNumber,
    string ExpirationYear,
    string ExpirationMonth,
    string Cvc);