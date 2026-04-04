using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using SurveyBasket.Api.Authentication;
using SurveyBasket.Api.persistence;
using SurveyBasket.Api.Services;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace SurveyBasket.Api
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddDependencies (this IServiceCollection services,IConfiguration configuration)
        {

           services.AddControllers();
            services.AddAuthConfig(configuration);

            var connectionString = configuration.GetConnectionString("DefaultConnaction") ??
                throw new InvalidOperationException("ConnectionString 'DefaultConnaction' Not Found ");
           services.AddDbContext<ApplicationDbContext>(options =>
            {
               options.UseSqlServer(connectionString);
            });


            services.AddSwaggerServices()
                    .AddMapsterConfig()
                    .AddFluentValidationConfig();


            services.AddScoped<IPollService, PollService>();
            services.AddScoped<IAuthService, AuthService>();
            return services;

        }


        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            services.AddOpenApi();
            services.AddScoped<IPollService, PollService>();
            return services;
        }

        private static IServiceCollection AddMapsterConfig(this IServiceCollection services)
        {
            //inject Mapster 
            var mappingConfig = TypeAdapterConfig.GlobalSettings;
            mappingConfig.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton<IMapper>(new Mapper(mappingConfig));
            return services;

        }
        private static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
        {
            // inject Fluent Validation
            //services.AddScoped<IValidator<CreateOrUpdatePollDto>, CreatePollValidator>();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();
            return services;
        }

        private static IServiceCollection AddAuthConfig(this IServiceCollection services, IConfiguration configuration)
        {
            // inject Fluent Validation
            //services.AddScoped<IValidator<CreateOrUpdatePollDto>, CreatePollValidator>();
            services.AddSingleton<IJWTProvider, JWTProvider>();
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:Key"]!)),
                    ValidIssuer = configuration["jwt:Issuer"],
                    ValidAudience = configuration["jwt:Audience"],
                };
            });

            return services;
        }

    }
}
