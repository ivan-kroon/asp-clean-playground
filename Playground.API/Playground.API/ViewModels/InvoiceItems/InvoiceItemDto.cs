namespace Playground.API.ViewModels.InvoiceItems
{
    public class InvoiceItemDto
    {
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal PricePerUnit { get; set; }
    }
}
