using Microsoft.EntityFrameworkCore;
using Playground.Core.Entities;
using Playground.Core.Interfaces;

namespace Playground.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IRepository<Invoice> _invoiceRepository;
        private readonly IRepository<Product> _productRepository;

        public InvoiceService(IRepository<Invoice> invoiceRepository,
            IRepository<Product> productRepository)
        {
            _invoiceRepository = invoiceRepository;
            _productRepository = productRepository;
        }

        public async Task<Invoice> GetInvoiceAsync(int id)
        {
            var invoice = await _invoiceRepository
                .SingleAsync(i => i.Where(x => x.Id == id)
                .Include(i => i.invoiceItems).ThenInclude(ia => ia.Product)
                .Include(i => i.Customer));

            return invoice;
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
        {
            var invoices = await _invoiceRepository
                .ListAsync(i => i
                .Include(i => i.invoiceItems).ThenInclude(ia => ia.Product)
                .Include(i => i.Customer));

            int i = 10;

            return invoices;
        }

        public async Task AddInvoiceAsync(Invoice invoice)
        {
            invoice.DateCreated = DateTime.Now;
            //calculate product price for every invoice item based on discount
            foreach (var item in invoice.invoiceItems)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                item.PricePerUnit = CalculatePricePerUnit(product.Price, item.DiscountPercent);
            }

            await _invoiceRepository.AddAsync(invoice);
        }

        private decimal CalculatePricePerUnit(decimal price, decimal discountPercent)
        {
            return price - (price / 100 * discountPercent);
        }

        public async Task Wait10SecAsync()
        {
            await Task.Delay(10000);
        }
    }
}
