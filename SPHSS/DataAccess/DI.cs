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
        services.AddScoped<IBlogService, BlogService>();
        


        #region Question/QuestionType
        services.AddScoped<IQuestionTypeService, QuestionTypeService>();
        services.AddScoped<IQuestionTypeRepo, QuestionTypeRepo>();
        services.AddScoped<IQuestionRepo, QuestionRepo>();
        services.AddScoped<IQuestionService, QuestionService>();
        #endregion

        #region Test
        services.AddScoped<ITestService, TestService>();
        #endregion

        return services;
    }
}
