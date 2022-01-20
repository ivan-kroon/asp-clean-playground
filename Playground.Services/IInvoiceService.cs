using Playground.Core.Entities;

namespace Playground.Services
{
    public interface IInvoiceService
    {
        Task<IEnumerable<Invoice>> GetAllInvoicesAsync();
        Task<Invoice> GetInvoiceAsync(int id);
        Task AddInvoiceAsync(Invoice invoice);
        Task Wait10SecAsync();
    }
}