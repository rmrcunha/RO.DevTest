using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RO.DevTest.Domain.Exception;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Application.Features.Sales.Commands.CreateSaleCommand;

public class CreateSaleCommandHandler(ISalesRepository salesRepository, IProductsRepository productsRepository): IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISalesRepository _salesRepository = salesRepository;

    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var product = await productsRepository.GetByIdAsync(request.ProductId);
        
        if(product == null) throw new Exception("Product not found");

        if(request.Quantity > product.Quantity) throw new BadRequestException("Not enough product quantity");

        var totalPrice = product.Price * request.Quantity;

        var newSale = new Sale();

        newSale.ProductId = request.ProductId;
        newSale.Quantity = request.Quantity;
        newSale.TotalPrice = totalPrice;
        newSale.CreatedAt = DateTime.UtcNow;


        await _salesRepository.CreateAsync(newSale);

        product.Quantity -= request.Quantity;

        await productsRepository.UpdateAsync(product);

        return new CreateSaleResult(newSale);
    }

}
