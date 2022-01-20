using AutoMapper;
using Playground.API.ViewModels.Invoice;
using Playground.Core.Entities;

namespace Playground.API.Mapping
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<Invoice, InvoiceDto>()
                .ForMember(io => io.DayNumber, opts => 
                {
                    opts.MapFrom(inv => (DateTime.Now - inv.DateCreated).Days);
                });

            CreateMap<InvoiceForCreationDto, Invoice>();
        }
    }
}
