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

            CreateMap<InvoiceItemForCreationDto, InvoiceItem>()
                .ForMember(i => i.Id, opt => opt.Ignore())
                .ForMember(i => i.InvoiceId, opt => opt.Ignore())
                .ForMember(i => i.Invoice, opt => opt.Ignore())
                .ForMember(i => i.Product, opt => opt.Ignore())
                .ForMember(i => i.PricePerUnit, opt => opt.Ignore());
        }
    }
}
