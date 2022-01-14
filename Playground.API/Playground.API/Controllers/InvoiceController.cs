using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Playground.API.ViewModels.Invoice;
using Playground.Core.Entities;
using Playground.Services;

namespace Playground.API.Controllers
{
    [Route("api/invoice")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IMapper mapper, IInvoiceService invoiceService)
        {
            _mapper = mapper;
            _invoiceService = invoiceService;
        }

        // GET: api/invoice
        [HttpGet]
        public async Task<IEnumerable<InvoiceDto>> GetInvoices()
        {
            var invoices = await _invoiceService.GetAllInvoicesAsync();
            var invoiceDtos = _mapper.Map<IEnumerable<InvoiceDto>>(invoices);

            return invoiceDtos;
        }

        // GET api/invoice/5-
        [HttpGet("{id}")]
        public async Task<InvoiceDto> GetInvoice(int id)
        {
            var invoice = await _invoiceService.GetInvoiceAsync(id);
            var invoiceDto = _mapper.Map<InvoiceDto>(invoice);

            return invoiceDto;
        }

        // POST api/invoice
        [HttpPost]
        public async Task<ActionResult<InvoiceDto>> Post(InvoiceForCreationDto invoiceForCreationDto)
        {
            var invoice = _mapper.Map<Invoice>(invoiceForCreationDto);
            await _invoiceService.AddInvoiceAsync(invoice);

            var createdInvoice = await _invoiceService.GetInvoiceAsync(invoice.Id);
            var invoiceDto = _mapper.Map<InvoiceDto>(createdInvoice);
            return CreatedAtAction("GetInvoice", new { id = createdInvoice.Id }, invoiceDto);
        }

        #region ToImplement
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
