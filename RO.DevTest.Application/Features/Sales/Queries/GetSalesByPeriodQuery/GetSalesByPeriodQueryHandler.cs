using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RO.DevTest.Application.Features.Sales.DTO;

namespace RO.DevTest.Application.Features.Sales.Queries.GetSalesByPeriodQuery;

public class GetSalesByPeriodQueryHandler(ISalesRepository salesRepository, IProductsRepository productRepository) : IRequestHandler<GetSalesByPeriodQuery, GetSalesByPeriodResult>
{
    public async Task<GetSalesByPeriodResult> Handle(GetSalesByPeriodQuery request, CancellationToken cancellationToken)
    {
        if(request.endDate < request.startDate) throw new ArgumentException("End date cannot be earlier than start date.");

        var salesList = await salesRepository.GetSalesByPeriodAsync(request.startDate, request.endDate, cancellationToken);

        var totalCount = salesList.Count();
        var totalAmount = salesList.Sum(s => s.TotalPrice);

        var revenuePerProduct = salesList.GroupBy(s => s.ProductId)
            .Select(g =>
            {
                var product = productRepository.Query()
                .FirstOrDefault(p => p.Id == g.Key);
                var name = product?.Name ?? "Unknown Product";
                return new ProductRevenueDTO(
                    g.Key,
                    name,
                    g.Sum(s => s.TotalPrice)
                    );
            }).ToList();

        var salesByPeriodResult = new GetSalesByPeriodResult{
            TotalSales = totalCount,
            TotalRevenue = totalAmount,
            ProductsRevenue = revenuePerProduct,
           
        };

        return salesByPeriodResult;
    }
}