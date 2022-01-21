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
    public class InvoiceServiceTests : IClassFixture<SharedDatabaseFixture>
    {
        IRepository<Invoice> invoiceRepository;
        IRepository<Product> productRepository;
        IInvoiceService invoiceService;
        private readonly SharedDatabaseFixture _fixture;

        public InvoiceServiceTests(SharedDatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [Repeat(200)]
        public async Task InvoiceService_ShouldReturnAllInvoices(int i)
        {
            using (var context = _fixture.CreateContext())
            {
                InitializeDependecies(context);
                var result = await invoiceService.GetAllInvoicesAsync();
                Assert.Equal(2, result.Count());
            }
        }

        [Theory]
        [Repeat(200)]
        public async Task InvoiceService_ShouldReturnCorrectInvoice(int i)
        {
            using (var context = _fixture.CreateContext())
            {
                InitializeDependecies(context);

                Invoice invoice = await invoiceService.GetInvoiceAsync(1);

                Assert.NotNull(invoice);
                Assert.Equal(1, invoice.Id);
            }
        }

        [Theory]
        [Repeat(200)]
        public async Task InvoiceService_ShouldCalculateCorrectPricePerUnit(int i)
        {
            using (var transaction = _fixture.Connection.BeginTransaction())
            {
                using (var context = _fixture.CreateContext(transaction))
                {
                    InitializeDependecies(context);

                    Invoice invoice = await invoiceService.GetInvoiceAsync(1);
                    invoice.Id = 0;

                    await invoiceService.AddInvoiceAsync(invoice);

                    //check values of calculated prices for every item in passed invoice object
                    Assert.Equal(80, invoice.invoiceItems.ElementAt(0).PricePerUnit);
                    Assert.Equal(400, invoice.invoiceItems.ElementAt(1).PricePerUnit);
                }
            }
        }

        private void InitializeDependecies(PlayDbContext context)
        {
            invoiceRepository = new EfRepository<Invoice>(context);
            productRepository = new EfRepository<Product>(context);
            invoiceService = new InvoiceService(invoiceRepository, productRepository);
        }
    }
}
