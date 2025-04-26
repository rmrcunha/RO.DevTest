using Microsoft.EntityFrameworkCore;
using Moq;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Features.Product.Queries.GetProductsQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Tests.Unit.Application.Features.Product.Queries;

public class GetProductsQueryHandlerTests
{
    [Fact]
    public async Task Handle_ValidQuery_ReturnsPaginatedProducts()
    {
        // Arrange
        var products = new List<Domain.Entities.Product>
            {
                new() { Name = "Produto A", Price = 10, Quantity = 5 },
                new() { Name = "Produto B", Price = 20, Quantity = 10 },
                new() { Name = "Produto C", Price = 30, Quantity = 15 }
            }.AsQueryable();

        var mockDbSet = new Mock<DbSet<Domain.Entities.Product>>();
        mockDbSet.As<IQueryable<Domain.Entities.Product>>().Setup(m => m.Provider).Returns(products.Provider);
        mockDbSet.As<IQueryable<Domain.Entities.Product>>().Setup(m => m.Expression).Returns(products.Expression);
        mockDbSet.As<IQueryable<Domain.Entities.Product>>().Setup(m => m.ElementType).Returns(products.ElementType);
        mockDbSet.As<IQueryable<Domain.Entities.Product>>().Setup(m => m.GetEnumerator()).Returns(products.GetEnumerator());

        var mockRepo = new Mock<IProductsRepository>();
        mockRepo.Setup(r => r.Query()).Returns(products);

        var handler = new GetProductQueryHandler(mockRepo.Object, true);
        var query = new GetProductsQuery { Page = 1, PageSize = 2 };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.TotalCount);
        Assert.Equal(2, result.Items.Count());
        Assert.Equal(2, result.TotalPages);
    }
}