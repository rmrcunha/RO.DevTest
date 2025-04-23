using RO.DevTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Contracts.Infrastructure;

public interface IJwtService
{
    string GenerateAccessToken(User user, IList<string> roles);
}
