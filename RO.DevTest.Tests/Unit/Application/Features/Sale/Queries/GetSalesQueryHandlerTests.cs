using Moq;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Features.Sales.Queries.GetSalesQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Tests.Unit.Application.Features.Sale.Queries;

public class GetSalesQueryHandlerTests
{
    [Fact]
    public async Task Handle_ValidQuery_ReturnsPaginatedSales()
    {
        // Arrange
        var sales = new List<Domain.Entities.Sale>
        {
            new() { Id = "1", ProductId = "p1", Quantity = 3, TotalPrice = 100, CreatedAt = DateTime.UtcNow.AddDays(-1) },
            new() { Id = "2", ProductId = "p2", Quantity = 2, TotalPrice = 50, CreatedAt = DateTime.UtcNow },
            new() { Id = "3", ProductId = "p3", Quantity = 1, TotalPrice = 20, CreatedAt = DateTime.UtcNow },
            //new() { Id = "4", ProductId = "p4", Quantity = 5, TotalPrice = 200, CreatedAt = DateTime.UtcNow.AddDays(-2) },
            //new() { Id = "5", ProductId = "p5", Quantity = 4, TotalPrice = 150, CreatedAt = DateTime.UtcNow.AddDays(-3) },
            //new() { Id = "6", ProductId = "p6", Quantity = 6, TotalPrice = 300, CreatedAt = DateTime.UtcNow.AddDays(-4) }

        }.AsQueryable();

        var mockRepo = new Mock<ISalesRepository>();
        mockRepo.Setup(r => r.Query()).Returns(sales);

        var handler = new GetSalesQueryHandler(mockRepo.Object, true);

        var request = new GetSalesQuery
        {
            Page = 1,
            PageSize = 2,
            SortBy = "quantity",
            SortDescending = false
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.TotalCount);
        Assert.Equal(2, result.Items.Count());
        Assert.Equal("2", result.TotalPages.ToString());
    }
}
