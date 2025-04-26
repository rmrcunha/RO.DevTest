using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Dashboard.Queries;

public class GetDashboardSummaryHandler(
    UserManager<Domain.Entities.User> userManager,
    ISalesRepository salesRepository,
    IProductsRepository productsRepository
) : IRequestHandler<GetDashboardSummary, DashboardSummaryResult>
{
    private readonly UserManager<Domain.Entities.User> _userManager = userManager;
    private readonly ISalesRepository _salesRepository = salesRepository;
    private readonly IProductsRepository _productsRepository = productsRepository;

    public async Task<DashboardSummaryResult> Handle(GetDashboardSummary request, CancellationToken cancellationToken)
    {
        var totalUsers = await _userManager.Users.CountAsync(cancellationToken);
        var totalSales = await _salesRepository.GetTotalSalesAsync(cancellationToken);
        var totalProducts = await _productsRepository.GetTotalProductsAsync(cancellationToken);
        return new DashboardSummaryResult
        {
            TotalUsers = totalUsers,
            TotalSales = totalSales,
            TotalProducts = totalProducts
        };
    }
}