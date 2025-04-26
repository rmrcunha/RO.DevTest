using Microsoft.EntityFrameworkCore;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Persistence.Repositories;

public class SalesRepository(DefaultContext context) : BaseRepository<Sale>(context), ISalesRepository
{
    public async Task<List<Sale>> GetSalesByPeriodAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        return await Context.Sales
            .Where(s => s.CreatedAt >= startDate && s.CreatedAt <= endDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetTotalSalesAsync(CancellationToken cancellationToken)
    {
        return await Context.Sales.CountAsync(cancellationToken);
    }
}
