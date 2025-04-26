using Moq;
using OpenQA.Selenium;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Features.Sales.Queries.GetSaleByIdQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Tests.Unit.Application.Features.Sale.Queries;

public class GetSaleByIdQueryHandlerTests
{
    private readonly Mock<ISalesRepository> _saleRepositoryMock;
    private readonly GetSaleByIdQueryHandler _handler;

    public GetSaleByIdQueryHandlerTests()
    {
        _saleRepositoryMock = new Mock<ISalesRepository>();
        _handler = new GetSaleByIdQueryHandler(_saleRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ValidId_ShouldReturnSale()
    {
        // Arrange
        var saleId = "123";
        var sale = new Domain.Entities.Sale
        {
            Id = saleId,
            ProductId = "prod-1",
            Quantity = 2,
            TotalPrice = 200.0,
            CreatedAt = DateTime.UtcNow
        };

        _saleRepositoryMock
            .Setup(repo => repo.GetByIdAsync(saleId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(sale);

        var query = new GetSaleByIdQuery(saleId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(saleId, result.Id);
        Assert.Equal(sale.ProductId, result.ProductId);
        Assert.Equal(sale.Quantity, result.Quantity);
        Assert.Equal(sale.TotalPrice, result.TotalPrice);
    }

    [Fact]
    public async Task Handle_InvalidId_ShouldThrowNotFoundException()
    {
        // Arrange
        var saleId = "invalid-id";

        _saleRepositoryMock
            .Setup(repo => repo.GetByIdAsync(saleId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Domain.Entities.Sale?)null);

        var query = new GetSaleByIdQuery(saleId);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _handler.Handle(query, CancellationToken.None));
    }
}
