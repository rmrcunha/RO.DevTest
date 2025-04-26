using Moq;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Features.Product.Commands.CreateProductCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Tests.Unit.Application.Features.Product.Commands;

public class CreateProductCommandHandlerTests
{
    private readonly Mock<IProductsRepository> _mockRepository;
    private readonly CreateProductCommandHandler _handler;

    public CreateProductCommandHandlerTests()
    {
        _mockRepository = new Mock<IProductsRepository>();
        _handler = new CreateProductCommandHandler(_mockRepository.Object);
    }

    [Fact(DisplayName = "Given valid product should create a new product")]
    public async Task Handle_ValidProduct_ShouldCreateNewProductResult()
    {
        // Arrange
        var command = new CreateProductCommand
        {
            Name = "Test Product",
            Description = "Test Description",
            Price = 10.0,
            Quantity = 10
        };

        _mockRepository.Setup(r => r.CreateAsync(It.IsAny<Domain.Entities.Product>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Domain.Entities.Product p, CancellationToken _) => p);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(command.Name, result.ProductName);
        _mockRepository.Verify(r=>r.CreateAsync(It.IsAny<Domain.Entities.Product>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
