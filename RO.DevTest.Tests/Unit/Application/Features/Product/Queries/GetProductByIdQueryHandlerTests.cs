using Moq;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Features.Product.Queries.GetProductByIdQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Tests.Unit.Application.Features.Product.Queries;

public class GetProductByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_ExistingProductId_ReturnsProduct()
    {
        // Arrange
        var productId = "123";
        var product = new Domain.Entities.Product
        {
            Id = productId,
            Name = "Test Product",
            Price = 10,
            Quantity = 5,
            Description = "Descrição"
        };

        var mockRepo = new Mock<IProductsRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        var handler = new GetProductByIdQueryHandler(mockRepo.Object);
        var query = new GetProductByIdQuery(productId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productId, result.Id);
        Assert.Equal(product.Name, result.Name);
    }
}
