using Customer = Stripe.Customer;
namespace PerfumeShop.Infrastructure.Services;

public sealed class PaymentService : IPaymentService
{
    private readonly ILogger<PaymentService> _logger;
    private readonly IUnitOfWork<SaleDbContext> _unitOfWork;
    private readonly ChargeService _chargeService;
    private readonly CustomerService _customerService;
    private readonly TokenService _tokenService;

    private record StripeCustomer(
        string Id,
        string Name,
        string Phone,
        string Email,
        string State,
        string City,
        string Line1,
        string PostalCode);

    private record StripePayment(
        string CustomerId,
        string ReceiptEmail,
        string Currency,
        long Amount,
        bool Paid);

    public PaymentService(
        ILogger<PaymentService> logger,
        IUnitOfWork<SaleDbContext> unitOfWork,
        ChargeService chargeService,
        CustomerService customerService,
        TokenService tokenService)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _chargeService = chargeService;
        _customerService = customerService;
        _tokenService = tokenService;     
    }

    public async Task<PaymentDetail> PayAsync(Payment payment, CancellationToken ct)
    {
        StripeCustomer stripeCustomer = await AddStripeCustomerAsync(payment.Buyer, ct);
        StripePayment stripePayment = await AddStripePaymentAsync(payment, stripeCustomer.Id, ct);
        return await UpdatePaymentDetail(stripePayment, payment.OrderId);
    }
    
    #region Stripe
    private async Task<StripeCustomer> AddStripeCustomerAsync(Buyer buyer, CancellationToken ct)
    {
        var tokenOptions = new TokenCreateOptions
        {
            Card = new TokenCardOptions
            {
                Name = buyer.PaymentCard.NameOwner,
                Number = buyer.PaymentCard.CardNumber,
                ExpYear = buyer.PaymentCard.ExpirationYear,
                ExpMonth = buyer.PaymentCard.ExpirationMonth,
                Cvc = buyer.PaymentCard.Cvc,
            }
        };
        Token stripeToken = await _tokenService.CreateAsync(options: tokenOptions, cancellationToken: ct);

        var customerOptions = new CustomerCreateOptions
        {
            Address = new AddressOptions
            {
                State = buyer.Address.State,
                City = buyer.Address.City,
                Line1 = buyer.Address.StreetAddress,
                PostalCode = buyer.Address.PostalCode,
            },
            Name = buyer.Name,
            Email = buyer.Email,
            Phone = buyer.Phone,
            Source = stripeToken.Id,
        };
        Customer createdCustomer = await _customerService.CreateAsync(options: customerOptions, cancellationToken: ct);

        return new StripeCustomer(
            createdCustomer.Id,
            createdCustomer.Name,
            createdCustomer.Email,
            createdCustomer.Phone,
            createdCustomer.Address.State,
			createdCustomer.Address.City,
			createdCustomer.Address.Line1,
            createdCustomer.Address.PostalCode);
    }

    private async Task<StripePayment> AddStripePaymentAsync(Payment payment, string stripeCustomerId, CancellationToken ct)
    {
        var paymentOptions = new ChargeCreateOptions
        { 
            Customer = stripeCustomerId,
            ReceiptEmail = payment.Buyer.Email,           
            Currency = payment.Currency,
            Amount = (long)payment.TotalPrice * 100,           
        };

        Charge createdPayment = await _chargeService.CreateAsync(options: paymentOptions, cancellationToken: ct);

		return new StripePayment(
              createdPayment.CustomerId,
              createdPayment.ReceiptEmail,
              createdPayment.Currency,
              createdPayment.Amount,
              createdPayment.Paid);
    }
    #endregion

    private async Task<PaymentDetail> UpdatePaymentDetail(StripePayment stripePayment, int orderId)
    {
        var paymentDetailRepository = _unitOfWork.GetRepository<PaymentDetail>();
        var paymentDetail = await paymentDetailRepository.GetFirstOrDefaultAsync(predicate: p => p.OrderId == orderId)
            ?? throw new NullReferenceException($"Payment detail not found with Order ID '{orderId}'.");

        if (stripePayment.Paid)
        {
            paymentDetail.SetPaymentStatus(PaymentStatuses.Approved);
            paymentDetail.SetPaymentDate(DateTime.UtcNow);
            paymentDetailRepository.Update(paymentDetail);

            _logger.LogInformation($"Order with ID '{orderId}' successfully paid.");
        }
        else
        {
            paymentDetail.SetPaymentStatus(PaymentStatuses.Reject);
            paymentDetailRepository.Update(paymentDetail);

            _logger.LogInformation($"Order payment with ID '{orderId}' was rejected.");
        }
        await _unitOfWork.SaveChangesAsync();

        return paymentDetail;
    }
}
