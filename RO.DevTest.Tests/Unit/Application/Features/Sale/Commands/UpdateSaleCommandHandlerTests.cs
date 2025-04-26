using Moq;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Features.Sales.Commands.UpdateSaleCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Tests.Unit.Application.Features.Sale.Commands
{
    public class UpdateSaleCommandHandlerTests
    {
        private readonly Mock<ISalesRepository> _saleRepositoryMock;
        private readonly UpdateSaleCommandHandler _handler;

        public UpdateSaleCommandHandlerTests()
        {
            _saleRepositoryMock = new Mock<ISalesRepository>();
            _handler = new UpdateSaleCommandHandler(_saleRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidSale_ShouldUpdateAndReturnResult()
        {
            // Arrange
            var saleId = "sale-001";
            var command = new UpdateSaleCommand
            {
                Id = saleId,
                ProductId = "prod-001",
                Quantity = 3,
                Price = 75.0
            };

            var existingSale = new Domain.Entities.Sale
            {
                Id = saleId,
                ProductId = "prod-001",
                Quantity = 1,
                TotalPrice = 25.0
            };

            _saleRepositoryMock.Setup(r => r.GetByIdAsync(saleId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingSale);

            _saleRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Domain.Entities.Sale>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            if(!result.Id.Equals(saleId)) throw new Exception("Result ID should be equal to saleId");
            if(!result.Quantity.Equals(3)) throw new Exception("Quantity should be equals to 3");
            if(!result.TotalPrice.Equals(75.0)) throw new Exception("Total price should be equals to 75.0");
        }

        [Fact]
        public async Task Handle_SaleNotFound_ShouldReturnNull()
        {
            // Arrange
            var command = new UpdateSaleCommand
            {
                Id = "non-existent-id",
                ProductId = "prod-001",
                Quantity = 3,
                Price = 75.0
            };

            _saleRepositoryMock.Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.Entities.Sale)null!);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            if(result == null) throw new Exception("Sale returning null");
        }
    }

}
