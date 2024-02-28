namespace ProjectPizzaPortal.Models
{
    public class ConfirmOrder
    {
        public string? OrderId { get; set; }
        public string? Pizza { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
    }
}
