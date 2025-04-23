using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Sales.Queries.GetSalesByPeriodQuery;

public record GetSalesByPeriodQuery(DateTime startDate, DateTime endDate) : IRequest<GetSalesByPeriodResult>;
