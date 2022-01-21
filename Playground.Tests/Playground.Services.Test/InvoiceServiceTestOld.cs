using Moq;
using Playground.Core.Entities;
using Playground.Core.Interfaces;
using Playground.Infrastructure.Data;
using Playground.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Playground.Tests.Playground.Services.Test
{
    public class InvoiceServiceTestsOld : PlaygroundTestBase
    {
        IRepository<Invoice> invoiceRepository;
        IRepository<Product> productRepository;
        IInvoiceService invoiceService;

        public InvoiceServiceTestsOld()
        {
            invoiceRepository = new EfRepository<Invoice>(testDbContext);
            productRepository = new EfRepository<Product>(testDbContext);
            invoiceService = new InvoiceService(invoiceRepository, productRepository);
        }

        [Theory]
        [Repeat(200)]
        public async Task InvoiceService_ShouldReturnAllInvoices(int i)
        {
            var result = await invoiceService.GetAllInvoicesAsync();

            Assert.Equal(2, result.Count());
        }

        [Theory]
        [Repeat(200)]
        public async Task InvoiceService_ShouldReturnCorrectInvoice(int i)
        {
            Invoice invoice = await invoiceService.GetInvoiceAsync(1);

            Assert.NotNull(invoice);
            Assert.Equal(1, invoice.Id);
        }

        [Theory]
        [Repeat(200)]
        public async Task InvoiceService_ShouldCalculateCorrectPricePerUnit(int i)
        {
            //Arrange
            Invoice invoice = await invoiceService.GetInvoiceAsync(1);
            invoice.Id = 0;

            //Act
            await invoiceService.AddInvoiceAsync(invoice);

            //Assert

            //check values of calculated prices for every item in passed invoice object
            Assert.Equal(80, invoice.invoiceItems.ElementAt(0).PricePerUnit);
            Assert.Equal(400, invoice.invoiceItems.ElementAt(1).PricePerUnit);
        }
    }
}
