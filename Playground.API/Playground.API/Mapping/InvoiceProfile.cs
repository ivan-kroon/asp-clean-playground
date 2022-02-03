using AutoMapper;
using Playground.API.ViewModels.Invoice;
using Playground.Core.Entities;
using System;

namespace Playground.API.Mapping
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<Invoice, InvoiceDto>()
                .ForMember(idto => idto.DayNumberAfterIssued, opt => opt.MapFrom(i => (DateTime.Now - i.DateCreated).Days));

            CreateMap<InvoiceForCreationDto, Invoice>()
                .ForMember(i => i.Id, opt => opt.Ignore())
                .ForMember(i => i.DateCreated, opt => opt.Ignore())
                .ForMember(i => i.Customer, opt => opt.Ignore());

            //send anonymus func with lamba expression(put in profile code block to work)
            //CreateMap<Invoice, InvoiceDto>()
            //    .ForMember(io => io.DayNumberAfterIssued, opts =>
            //    {
            //        opts.MapFrom<int>((i, idto) =>
            //        {
            //            return (i.DateCreated - DateTime.Today).Days;
            //        });
            //    });
        }


        ////send function that is defined below
        //CreateMap<Invoice, InvoiceDto>()
        //    .ForMember(io => io.DayNumberAfterIssued, opts =>
        //    {
        //        opts.MapFrom<int>(MapDateCreatedToDayNumber);
        //    });

        //private int MapDateCreatedToDayNumber(Invoice invoice, InvoiceDto invoiceDto)
        //{
        //    return (DateTime.Now - invoice.DateCreated).Days;
        //}
    }
}
