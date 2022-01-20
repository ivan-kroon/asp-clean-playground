using Playground.API.ViewModels.InvoiceItems;

namespace Playground.API.ViewModels.Invoice
{
    public class InvoiceDto
    {
        public string InvoiceNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public string CustomerName { get; set; }
        public int DayNumber { get; set; }
        public IEnumerable<InvoiceItemDto> invoiceItems { get; set; } = new List<InvoiceItemDto>();
    }
}
