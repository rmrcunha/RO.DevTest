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
    public Task<IEnumerable<Sale>> GetSalesByCustomerIdAsync(int customerId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Sale>> GetSalesByProductIdAsync(int productId)
    {
        throw new NotImplementedException();
    }
}
