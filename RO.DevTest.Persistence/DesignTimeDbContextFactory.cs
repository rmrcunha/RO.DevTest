using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Persistence;

public class DesignTimeDbContextFactory: IDesignTimeDbContextFactory<DefaultContext>
{
    public DefaultContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DefaultContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=ro_devtestDB;Username=postgres;Password=123456");
        return new DefaultContext(optionsBuilder.Options);
    }
}
