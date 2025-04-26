using Moq;
using OpenQA.Selenium;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Features.Product.Commands.UpdateProductCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Tests.Unit.Application.Features.Product.Commands;

public class UpdateProductCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidCommand_UpdatesProductSuccessfully()
    {
        // Arrange
        var productId = "123";
        var existingProduct = new Domain.Entities.Product
        {
            Id = productId,
            Name = "Original",
            Price = 10,
            Quantity = 5,
            Description = "Original Desc"
        };

        var command = new UpdateProductCommand
        {
            Id = productId,
            Name = "Updated Name",
            Price = 20,
            Quantity = 10,
            Description = "Updated Desc"
        };

        var mockRepo = new Mock<IProductsRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingProduct);
        mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Domain.Entities.Product>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new UpdateProductCommandHandler(mockRepo.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(command.Name, result.ProductName);
        Assert.Equal(command.Price, result.ProductPrice);
        Assert.Equal(command.Quantity, result.ProductQuantity);
    }

    [Fact]
    public async Task Handle_NonExistingProduct_ThrowsNotFoundException()
    {
        // Arrange
        var command = new UpdateProductCommand { Id = "notfound" };

        var mockRepo = new Mock<IProductsRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Domain.Entities.Product?)null);

        var handler = new UpdateProductCommandHandler(mockRepo.Object);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(command, CancellationToken.None));
    }
}