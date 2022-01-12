namespace Playground.Core.Entities
{
    public class Invoice : BaseEntity
    {
        public DateTime DateCreated { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public IEnumerable<InvoiceItem> invoiceItems { get; set; }
    }
}
