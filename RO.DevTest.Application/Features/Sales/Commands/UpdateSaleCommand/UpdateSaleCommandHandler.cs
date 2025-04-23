using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Sales.Commands.UpdateSaleCommand;

public class UpdateSaleCommandHandler(ISalesRepository salesRepository) : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        UpdateSaleCommandValidator validator = new();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid) throw new BadRequestException(validationResult);

        var sale = await salesRepository.GetByIdAsync(request.Id, cancellationToken);
        if (sale == null) throw new Exception($"Sale with id {request.Id} not found");

        sale.ProductId = request.ProductId;
        sale.Quantity = request.Quantity;
        sale.TotalPrice = request.Price;

        await salesRepository.UpdateAsync(sale, cancellationToken);

        return new UpdateSaleResult
        (
            sale.Id,
            sale.ProductId,
            sale.Quantity,
            sale.TotalPrice,
            sale.CreatedAt
        );
    }
}