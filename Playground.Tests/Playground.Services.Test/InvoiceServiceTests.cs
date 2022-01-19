using Moq;
using Playground.Core.Entities;
using Playground.Core.Interfaces;
using Playground.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Playground.Tests.Playground.Services.Test
{
    public class InvoiceServiceTests
    {
        Product product1;
        Product product2;
        Product product3;

        Customer customer1;
        Customer customer2;

        Invoice invoice1;
        Invoice invoice2;

        InvoiceItem invoiceItem1;
        InvoiceItem invoiceItem2;
        InvoiceItem invoiceItem3;
        InvoiceItem invoiceItem4;

        Mock<IRepository<Invoice>> invoiceRepositoryMock;
        Mock<IRepository<Product>> productRepositoryMock;

        private void InitializeTestData()
        {
            #region Products
            product1 = new Product
            {
                Name = "Beer",
                Price = 100,
                Description = ""
            };

            product2 = new Product
            {
                Name = "Vodka",
                Price = 500,
                Description = ""
            };

            product3 = new Product
            {
                Name = "Tequila",
                Price = 1200,
                Description = ""
            };
            #endregion

            #region Customers
            customer1 = new Customer
            {
                Name = "Beer pub",
                Address = "Beer pub address",
                Email = "beerpub@gmail.com",
                IsRetail = false
            };

            customer2 = new Customer
            {
                Name = "Ivan",
                Address = "Ivan Address",
                Email = "ivan@gmail.com",
                IsRetail = true
            };
            #endregion

            #region InvoiceItems
            invoiceItem1 = new InvoiceItem
            {
                Invoice = invoice1,
                ProductId = 1,
                Product = product1,
                Quantity = 10,
                DiscountPercent = 20
            };

            invoiceItem2 = new InvoiceItem
            {
                Invoice = invoice1,
                ProductId = 2,
                Product = product2,
                Quantity = 2,
                DiscountPercent = 20
            };

            invoiceItem3 = new InvoiceItem
            {
                Invoice = invoice2,
                ProductId = 1,
                Product = product1,
                Quantity = 20,
                DiscountPercent = 10
            };

            invoiceItem4 = new InvoiceItem
            {
                Invoice = invoice2,
                ProductId = 3,
                Product = product3,
                Quantity = 3,
                DiscountPercent = 10
            };
            #endregion

            #region Invoices
            invoice1 = new Invoice
            {
                InvoiceNumber = "01/2022",
                Customer = customer1,
                DateCreated = DateTime.Today,
                invoiceItems = new List<InvoiceItem>() {invoiceItem1, invoiceItem2}
            };

            invoice2 = new Invoice
            {
                InvoiceNumber = "02/2022",
                Customer = customer2,
                DateCreated = DateTime.Today,
                invoiceItems= new List<InvoiceItem>() {invoiceItem2, invoiceItem3} 
            };
            #endregion
        }


        public InvoiceServiceTests()
        {
            InitializeTestData();
            invoiceRepositoryMock = new Mock<IRepository<Invoice>>();
            productRepositoryMock = new Mock<IRepository<Product>>();
        }

        [Fact]
        public async Task InvoiceService_ShouldReturnAllInvoices()
        {
            invoiceRepositoryMock.Setup(m => m.ListAsync(It.IsAny<Func<IQueryable<Invoice>, IQueryable<Invoice>>>()))
                .Returns(Task.FromResult((IEnumerable<Invoice>)new List<Invoice>() { invoice1, invoice2})).Verifiable();

            IInvoiceService invoiceService = new InvoiceService(invoiceRepositoryMock.Object, productRepositoryMock.Object);

            var result = await invoiceService.GetAllInvoicesAsync();

            invoiceRepositoryMock.Verify();
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task InvoiceService_ShouldReturnCorrectInvoice()
        {
            //var funcMock = new Mock<Func<IQueryable<Invoice>, IQueryable<Invoice>>>();

            invoiceRepositoryMock.Setup(m => m.SingleAsync(It.IsAny<Func<IQueryable<Invoice>, IQueryable<Invoice>>>()))
                .Returns(Task.FromResult(invoice1)).Verifiable();

            IInvoiceService invoiceService = new InvoiceService(invoiceRepositoryMock.Object, productRepositoryMock.Object);

            Invoice invoice = await invoiceService.GetInvoiceAsync(1);

            invoiceRepositoryMock.Verify();
            Assert.NotNull(invoice);
        }

        [Fact]
        public async Task InvoiceService_ShouldCalculateCorrectPricePerUnit()
        {
            //Arrange
            productRepositoryMock.Setup(p => p.GetByIdAsync(1)).Returns(Task.FromResult(product1));
            productRepositoryMock.Setup(p => p.GetByIdAsync(2)).Returns(Task.FromResult(product2));

            IInvoiceService invoiceService = new InvoiceService(invoiceRepositoryMock.Object, productRepositoryMock.Object);
            
            //Act
            await invoiceService.AddInvoiceAsync(invoice1);

            //Assert

            //check values of calculated prices for every item in passed invoice object
            Assert.Equal(80, invoice1.invoiceItems.ElementAt(0).PricePerUnit);
            Assert.Equal(400, invoice1.invoiceItems.ElementAt(1).PricePerUnit);

            //check if this repository function is called with passed object
            invoiceRepositoryMock.Verify(i => i.AddAsync(invoice1));
        }
    }
}
