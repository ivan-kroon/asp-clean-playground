using AutoMapper;
using Playground.API.ViewModels.InvoiceItems;
using Playground.Core.Entities;

namespace Playground.API.Mapping
{
    public class InvoiceItemProfile : Profile
    {
        public InvoiceItemProfile()
        {
            CreateMap<InvoiceItem, InvoiceItemDto>();
        }
    }
}
