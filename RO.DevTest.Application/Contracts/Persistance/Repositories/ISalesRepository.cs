using RO.DevTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Contracts.Persistance.Repositories;

public interface ISalesRepository:IBaseRepository<Sale>
{
    Task<IEnumerable<Sale>> GetSalesByProductIdAsync(int productId);
    Task<IEnumerable<Sale>> GetSalesByCustomerIdAsync(int customerId);
}