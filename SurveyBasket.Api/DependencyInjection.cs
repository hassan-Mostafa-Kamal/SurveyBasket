using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using SurveyBasket.Api.persistence;
using SurveyBasket.Api.Services;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SurveyBasket.Api
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddDependencies (this IServiceCollection services,IConfiguration configuration)
        {

           services.AddControllers();

            var connectionString = configuration.GetConnectionString("DefaultConnaction") ??
                throw new InvalidOperationException("ConnectionString 'DefaultConnaction' Not Found ");
           services.AddDbContext<ApplicationDbContext>(options =>
            {
               options.UseSqlServer(connectionString);
            });


            services.AddSwaggerServices()
                    .AddMapsterConfig()
                    .AddFluentValidationConfig();
            return services;

        }


        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            services.AddOpenApi();
            services.AddScoped<IPollService, PollService>();
            return services;
        }

        public static IServiceCollection AddMapsterConfig(this IServiceCollection services)
        {
            //inject Mapster 
            var mappingConfig = TypeAdapterConfig.GlobalSettings;
            mappingConfig.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton<IMapper>(new Mapper(mappingConfig));
            return services;

        }
        public static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
        {
            // inject Fluent Validation
            //services.AddScoped<IValidator<CreateOrUpdatePollDto>, CreatePollValidator>();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();
            return services;
        }

    }
}
