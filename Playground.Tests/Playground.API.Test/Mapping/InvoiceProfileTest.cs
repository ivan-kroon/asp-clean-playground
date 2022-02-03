using AutoMapper;
using Moq;
using Playground.API.Mapping;
using Playground.API.ViewModels.Invoice;
using Playground.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Playground.Tests.Playground.API.Test.Mapping
{
    public class InvoiceProfileTest
    {
        MapperConfiguration _config;
        IMapper _mapper;

        public InvoiceProfileTest()
        {
            _config = new MapperConfiguration(cfg => cfg.AddProfiles(new List<Profile>() { new InvoiceProfile(), new InvoiceItemProfile() }));
            _mapper = _config.CreateMapper();
        }

        [Fact]
        public void InvoiceMapper_ConfigurationValid()
        {
            _config.AssertConfigurationIsValid();
        }

        [Fact]
        public void InvoiceMapper_DayNumberAfterIssuedCalculationValid()
        {
            var invoice = Mock.Of<Invoice>();
            invoice.DateCreated = DateTime.Now.AddDays(-5);

            var invoiceDto = _mapper.Map<InvoiceDto>(invoice);

            Assert.NotNull(invoiceDto);
            Assert.Equal(5, invoiceDto.DayNumberAfterIssued);
        }
    }
}
