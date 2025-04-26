namespace SportsApp.API.Models
{
    public class Payment
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "CLP"; // or "USD" for Stripe default
        public string? PaymentProvider { get; set; } // "Stripe", "WebPay", etc.
        public string? ProviderPaymentId { get; set; } // Stripe PaymentIntentId or WebPay transaction token
        public bool Success { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
