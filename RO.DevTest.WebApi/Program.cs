using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RO.DevTest.Application;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities;
using RO.DevTest.Infrastructure.IoC;
using RO.DevTest.Persistence;
using RO.DevTest.Persistence.IoC;

namespace RO.DevTest.WebApi;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<DefaultContext>()
            .AddDefaultTokenProviders();


        // Add Mediatr to program
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(
                typeof(ApplicationLayer).Assembly,
                typeof(Program).Assembly
            );
        });

        builder.Services.AddInfrastructureDependencies();
        builder.Services.AddPersistenceDependencies(builder.Configuration);

        builder.Services.AddDbContext<DefaultContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if(app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
