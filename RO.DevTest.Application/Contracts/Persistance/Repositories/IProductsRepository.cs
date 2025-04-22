using RO.DevTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Contracts.Persistance.Repositories;

public interface IProductsRepository : IBaseRepository<Product> { }