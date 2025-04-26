using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Sales.Queries.GetSalesQuery;

public record GetSalesQuery
(
    int Page = 1,
    int PageSize = 10,
    string? SortBy = null,
    bool SortDescending = false
    ) :IRequest<GetSalesResult>;
