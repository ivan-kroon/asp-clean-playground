namespace Playground.Core.Entities
{
    public class InvoiceItem : BaseEntity
    {
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal PricePerUnit { get; set; }
    }
}
