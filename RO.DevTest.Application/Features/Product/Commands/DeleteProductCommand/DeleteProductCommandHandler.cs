using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.Product.Commands.DeleteProductCommand;

public class DeleteProductCommandHandler(IProductsRepository productsRepository) : IRequestHandler<DeleteProductCommand>
{
    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productsRepository.GetByIdAsync(request.Id, cancellationToken);

        if(product == null) throw new Exception($"Product with id {request.Id} not found");

        await productsRepository.DeleteAsync(product, cancellationToken);

        return Unit.Value;
    }
}
