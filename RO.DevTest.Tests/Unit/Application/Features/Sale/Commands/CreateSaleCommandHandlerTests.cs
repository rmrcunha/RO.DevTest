using Moq;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Features.Sales.Commands.CreateSaleCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Tests.Unit.Application.Features.Sale.Commands;

public class CreateSaleCommandHandlerTests
{
    private readonly Mock<ISalesRepository> _mockSaleRepository;
    private readonly Mock<IProductsRepository> _mockProductRepository;
    private readonly CreateSaleCommandHandler _handler;

    public CreateSaleCommandHandlerTests()
    {
        _mockSaleRepository = new Mock<ISalesRepository>();
        _mockProductRepository = new Mock<IProductsRepository>();
        _handler = new CreateSaleCommandHandler(_mockSaleRepository.Object, _mockProductRepository.Object);
    }
    public async Task Handle_ValidRequest_ShouldReturnCreateSaleResult()
    {
        // Arrange
        var command = new CreateSaleCommand
        {
            ProductId = "product-123",
            Quantity = 2
        };

        var fakeProduct = new Domain.Entities.Product
        {
            Id = command.ProductId,
            Name = "Produto Teste",
            Price = 50.0,
            Quantity = 10
        };

        _mockProductRepository
        .Setup(repo => repo.GetByIdAsync(command.ProductId, It.IsAny<CancellationToken>()))
        .ReturnsAsync(fakeProduct);

        _mockSaleRepository
            .Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Sale>(), It.IsAny<CancellationToken>()))
            .Returns((Task<Domain.Entities.Sale>)Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(command.ProductId, result.ProductId);
        Assert.Equal(command.Quantity, result.Quantity);

        _mockSaleRepository.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Sale>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockProductRepository.Verify(repo => repo.GetByIdAsync(command.ProductId, It.IsAny<CancellationToken>()), Times.Once);
    }
}
