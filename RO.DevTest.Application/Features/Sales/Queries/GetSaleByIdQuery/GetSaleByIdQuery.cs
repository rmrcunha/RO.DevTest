using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Sales.Queries.GetSaleByIdQuery;

public record GetSaleByIdQuery(string id): IRequest<GetSaleByIdResult>;
