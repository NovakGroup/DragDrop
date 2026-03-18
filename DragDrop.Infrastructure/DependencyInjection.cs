using DragDrop.Application.Interfaces;
using DragDrop.Application.Services;
using DragDrop.Domain.Interfaces;
using DragDrop.Infrastructure.Data;
using DragDrop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DragDrop.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IDragItemRepository, DragItemRepository>();
        services.AddScoped<IDragItemService, DragItemService>();

        return services;
    }
}
