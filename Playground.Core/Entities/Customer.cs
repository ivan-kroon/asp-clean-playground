namespace Playground.Core.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool IsRetail { get; set; }
        public IEnumerable<Invoice> Invoices { get; set; }
    }
}
