namespace PerfumeShop.Infrastructure.Services;

public sealed class PaymentService : IPaymentService
{
    private readonly ChargeService _chargeService;
    private readonly CustomerService _customerService;
    private readonly TokenService _tokenService;

    public PaymentService(
        ChargeService chargeService,
        CustomerService customerService,
        TokenService tokenService)
    {
        _chargeService = chargeService;
        _customerService = customerService;
        _tokenService = tokenService;     
    }

    public async Task PayAsync(Buyer buyer, OrderHeader order)
    {
        var token = await AddStripeTokenAsync(buyer.CreditCard);
        var customer = await AddStripeCustomerAsync(buyer, token);
    }

    private async Task<Token> AddStripeTokenAsync(PaymentCard creditCard)
    {
        var tokenOptions = new TokenCreateOptions
        {
            Card = new TokenCardOptions
            {
                Name = creditCard.NameOwner,
                Number = creditCard.CardNumber,
                ExpYear = creditCard.ExpirationYear,
                ExpMonth = creditCard.ExpirationMonth,
                Cvc = creditCard.Cvc,
            }
        };
        return await _tokenService.CreateAsync(tokenOptions);
    }

    private async Task<Customer> AddStripeCustomerAsync(Buyer buyer, Token token)
    {
        var customerOptions = new CustomerCreateOptions
        {
            Name = buyer.Name,
            Email = buyer.Email,
            Source = token.Id,
        };

        return await _customerService.CreateAsync(customerOptions);
    }
}
