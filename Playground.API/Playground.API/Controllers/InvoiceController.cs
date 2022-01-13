using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Playground.API.ViewModels.Invoice;
using Playground.Core.Entities;
using Playground.Core.Interfaces;

namespace Playground.API.Controllers
{
    [Route("api/invoice")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IRepository<Invoice> _invoiceRepository;
        private readonly IMapper _mapper;

        public InvoiceController(IRepository<Invoice> invoiceRepository, IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
        }

        // GET: api/invoice
        [HttpGet]
        public async Task<IEnumerable<InvoiceDto>> GetInvoices()
        {
            var invoices = await _invoiceRepository.ListAsync();
            var result = _mapper.Map<IEnumerable<InvoiceDto>>(invoices);

            return result;
        }

        // GET api/invoice/5-
        [HttpGet("{id}")]
        public async Task<InvoiceDto> GetInvoice(int id)
        {
            var inv = await _invoiceRepository.SingleAsync(i => i.Where(x => x.Id == id)
                .Include(i => i.invoiceItems).ThenInclude(ia => ia.Product)
                .Include(i => i.Customer));

            var result = _mapper.Map<InvoiceDto>(inv);

            return result;
        }

        #region toImplement
        // POST api/invoice
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/invoice/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/invoice/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        #endregion
    }
}
