namespace Playground.API.ViewModels.InvoiceItems
{
    public class InvoiceItemForCreationDto
    {
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal DiscountPercent { get; set; }
    }
}
