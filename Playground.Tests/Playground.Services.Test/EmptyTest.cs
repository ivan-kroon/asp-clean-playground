using Moq;
using Playground.Core.Entities;
using Playground.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Playground.Tests.Playground.Services.Test
{
    public class EmptyTest
    {

        [Theory]
        [Repeat(30)]
        public async Task InvoiceService_ShouldReturnCorrectInvoice(int i)
        {
            var inv = Mock.Of<Invoice>();
            inv.Id = 1;

            var invoiceService = Mock.Of<IInvoiceService>();
            Mock.Get(invoiceService).Setup(s => s.GetInvoiceAsync(1)).ReturnsAsync(inv);

            var result = await invoiceService.GetInvoiceAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }
    }
}
