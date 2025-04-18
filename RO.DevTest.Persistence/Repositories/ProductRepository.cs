using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Persistence.Repositories;

public class ProductRepository(DefaultContext context): BaseRepository<Product>(context), IProductsRepository { }
