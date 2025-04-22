using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Product.Queries.GetProductByIdQuery;

public record GetProductByIdResult
(
    string Id,
    string Name,
    string Description,
    double Price,
    int Quantity,
    string CreatedAt,
    string UpdatedAt
);
