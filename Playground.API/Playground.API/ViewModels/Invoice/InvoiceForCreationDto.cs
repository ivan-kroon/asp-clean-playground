using Playground.API.ViewModels.InvoiceItems;

namespace Playground.API.ViewModels.Invoice
{
    public class InvoiceForCreationDto
    {
        public string InvoiceNumber { get; set; }
        public int CustomerId { get; set; }
        public IEnumerable<InvoiceItemForCreationDto> invoiceItems { get; set; } = new List<InvoiceItemForCreationDto>();
    }
}
