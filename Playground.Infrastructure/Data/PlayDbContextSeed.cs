using Microsoft.EntityFrameworkCore;
using Playground.Core.Entities;

namespace Playground.Infrastructure.Data
{
    public class PlayDbContextSeed
    {
        public static async Task CreateAndSeedDatabaseAsync(PlayDbContext context)
        {
            //just remove database in dev envirorment
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            #region Products
            var product1 = new Product
            {
                Name = "Beer",
                Price = 100,
                Description = ""
            };

            var product2 = new Product
            {
                Name = "Vodka",
                Price = 500,
                Description = ""
            };

            var product3 = new Product
            {
                Name = "Tequila",
                Price = 1200,
                Description = ""
            };
            #endregion

            #region Customers
            var customer1 = new Customer
            {
                Name = "Beer pub",
                Address = "Beer pub address",
                Email = "beerpub@gmail.com",
                IsRetail = false
            };

            var customer2 = new Customer
            {
                Name = "Ivan",
                Address = "Ivan Address",
                Email = "ivan@gmail.com",
                IsRetail = true
            };
            #endregion

            #region Invoices
            var invoice1 = new Invoice
            {
                InvoiceNumber = "01/2022",
                Customer = customer1,
                DateCreated = DateTime.Today.AddDays(-5),
            };

            var invoice2 = new Invoice
            {
                InvoiceNumber = "02/2022",
                Customer = customer2,
                DateCreated = DateTime.Today,
            };
            #endregion

            #region InvoiceItems
            var invoiceItem1 = new InvoiceItem
            {
                Invoice = invoice1,
                Product = product1,
                Quantity = 10,
                DiscountPercent = 20,
                PricePerUnit = 80
            };

            var invoiceItem2 = new InvoiceItem
            {
                Invoice = invoice1,
                Product = product2,
                Quantity = 2,
                DiscountPercent = 20,
                PricePerUnit = 400
            };

            var invoiceItem3 = new InvoiceItem
            {
                Invoice = invoice2,
                Product = product1,
                Quantity = 20,
                DiscountPercent= 10,
                PricePerUnit = 90
            };

            var invoiceItem4 = new InvoiceItem
            {
                Invoice = invoice2,
                Product = product3,
                Quantity = 3,
                DiscountPercent = 10,
                PricePerUnit = 1080
            };
            #endregion

            if (!await context.Products.AnyAsync())
            {
                await context.Products.AddRangeAsync(product1, product2, product3);
            }

            if (!await context.Customers.AnyAsync())
            {
                await context.Customers.AddRangeAsync(customer1, customer2);
            }

            if (!await context.Invoices.AnyAsync())
            {
                await context.Invoices.AddRangeAsync(invoice1, invoice2);
            }

            if (!await context.InvoiceItems.AnyAsync())
            {
                await context.InvoiceItems.AddRangeAsync(invoiceItem1, invoiceItem2, invoiceItem3, invoiceItem4);
            }

            await context.SaveChangesAsync();
        }
    }
}
