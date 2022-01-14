using AutoMapper;
using Playground.API.ViewModels.Invoice;
using Playground.Core.Entities;

namespace Playground.API.Mapping
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<Invoice, InvoiceDto>();
            CreateMap<InvoiceForCreationDto, Invoice>();
        }
    }
}
