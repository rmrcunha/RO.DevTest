﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Product.Commands.DeleteProductCommand;

public record DeleteProductCommand(string Id): IRequest<Unit>;
