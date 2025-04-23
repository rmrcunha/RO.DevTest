using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Sales.Commands.DeleteSaleCommand;

public class DeleteSaleCommandHandler(ISalesRepository saleRepository) : IRequestHandler<DeleteSaleCommand, bool>
{
    private readonly ISalesRepository _saleRepository = saleRepository;
    public async Task<bool> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await saleRepository.GetByIdAsync(request.Id);

        if (sale == null) throw new Exception($"Sale with ID {request.Id} doesn't exist");

        await _saleRepository.DeleteAsync(sale, cancellationToken);
        return true;
    }
}