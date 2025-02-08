using BusinessObject;
using DataAccess.Repo.IRepo;
using DataAccess.Repo;
using DataAccess.Service.IService;
using DataAccess.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

public static class DI
{
    public static IServiceCollection AddService(this IServiceCollection services, string? DatabaseConnection)
    {

        services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));
        services.AddScoped<IAccountService, AccountService>();

        return services;
    }
}
