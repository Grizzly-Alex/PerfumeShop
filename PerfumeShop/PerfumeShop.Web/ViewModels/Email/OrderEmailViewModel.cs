namespace PerfumeShop.Web.ViewModels.Email
{
    public sealed class OrderEmailViewModel
    {
        public DateTime OrderDate { get; set; }
        public string DateFormat => OrderDate.ToString("MM/dd/yyyy hh:mm tt");
        public string TrackingId { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public decimal TotalPrice { get; private set; }
        public string TotalPriceFormat => TotalPrice.ToString("c");
        public string PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }
        public string DeliveryMethod { get; set; }
        public IList<OrderItemViewModel> OrderItems { get; set; }
    }
}
