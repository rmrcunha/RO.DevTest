﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Sales.Commands.DeleteSaleCommand;

public record DeleteSaleCommand(string Id):IRequest<bool>;
