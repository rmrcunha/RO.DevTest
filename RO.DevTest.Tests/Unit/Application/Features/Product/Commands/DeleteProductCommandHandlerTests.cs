using Moq;
using OpenQA.Selenium;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Features.Product.Commands.DeleteProductCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Tests.Unit.Application.Features.Product.Commands;

public class DeleteProductCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidId_DeletesProductSuccessfully()
    {
        // Arrange
        var productId = "prod-123";
        var existingProduct = new Domain.Entities.Product { Id = productId };

        var mockRepo = new Mock<IProductsRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingProduct);
        mockRepo.Setup(r => r.DeleteAsync(existingProduct, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

        var handler = new DeleteProductCommandHandler(mockRepo.Object);

        // Act
        var result = await handler.Handle(new DeleteProductCommand( productId ), CancellationToken.None);

        // Assert
        mockRepo.Verify(r => r.GetByIdAsync(productId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidId_ThrowsNotFoundException()
    {
        // Arrange
        var productId = "invalid-123";

        var mockRepo = new Mock<IProductsRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.Entities.Product?)null);

        var handler = new DeleteProductCommandHandler(mockRepo.Object);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(new DeleteProductCommand(productId), CancellationToken.None));
    }
}