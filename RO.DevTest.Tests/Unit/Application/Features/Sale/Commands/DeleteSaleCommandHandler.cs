using Moq;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Features.Sales.Commands.DeleteSaleCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Tests.Unit.Application.Features.Sale.Commands;

public class DeleteSaleCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidId_ShouldDeleteSale()
    {
        // Arrange
        var saleId = "sale-789";
        var sale = new Domain.Entities.Sale
        {
            Id = saleId,
            ProductId = "prod-001",
            Quantity = 1,
            TotalPrice = 25.0
        };

        var mockRepo = new Mock<ISalesRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(saleId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(sale);

        mockRepo.Setup(r => r.DeleteAsync(sale, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

        var handler = new DeleteSaleCommandHandler(mockRepo.Object);
        var command = new DeleteSaleCommand(saleId );

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Equals(true);
        mockRepo.Verify(r => r.DeleteAsync(It.Is<Domain.Entities.Sale>(s => s.Id == saleId), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidId_ShouldReturnFalse()
    {
        // Arrange
        var mockRepo = new Mock<ISalesRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.Entities.Sale?)null);

        var handler = new DeleteSaleCommandHandler(mockRepo.Object);
        var command = new DeleteSaleCommand("invalid-id");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Equals(false);
        mockRepo.Verify(r => r.DeleteAsync(It.IsAny<Domain.Entities.Sale>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}