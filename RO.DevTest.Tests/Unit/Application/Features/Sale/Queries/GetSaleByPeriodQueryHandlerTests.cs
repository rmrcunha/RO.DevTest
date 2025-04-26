using Moq;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Features.Sales.Queries.GetSalesByPeriodQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Tests.Unit.Application.Features.Sale.Queries;

public class GetSalesByPeriodQueryHandlerTests
{
    private readonly Mock<ISalesRepository> _saleRepositoryMock;
    private readonly Mock<IProductsRepository> _productRepositoryMock;
    private readonly GetSalesByPeriodQueryHandler _handler;

    public GetSalesByPeriodQueryHandlerTests()
    {
        _saleRepositoryMock = new Mock<ISalesRepository>();
        _productRepositoryMock = new Mock<IProductsRepository>();
        _handler = new GetSalesByPeriodQueryHandler(_saleRepositoryMock.Object, _productRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ValidPeriod_ShouldReturnSalesList()
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddDays(-5);
        var endDate = DateTime.UtcNow;
        var sales = new List<Domain.Entities.Sale>
            {
                new Domain.Entities.Sale { Id = "1", ProductId = "P1", Quantity = 1, TotalPrice = 100, CreatedAt = DateTime.UtcNow.AddDays(-3) },
                new Domain.Entities.Sale { Id = "2", ProductId = "P2", Quantity = 2, TotalPrice = 200, CreatedAt = DateTime.UtcNow.AddDays(-2) }
            };

        _saleRepositoryMock
            .Setup(repo => repo.GetSalesByPeriodAsync(startDate, endDate, It.IsAny<CancellationToken>()))
            .ReturnsAsync(sales);

        var query = new GetSalesByPeriodQuery(startDate, endDate);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.TotalSales);
    }

    [Fact]
    public async Task Handle_EmptyPeriod_ShouldReturnEmptyList()
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddDays(-5);
        var endDate = DateTime.UtcNow;

        _saleRepositoryMock
            .Setup(repo => repo.GetSalesByPeriodAsync(startDate, endDate, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Domain.Entities.Sale>());

        var query = new GetSalesByPeriodQuery(startDate, endDate);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.ProductsRevenue);
    }
}