using System.Text;
using CleanArcTemp.Application.Common.Interfaces;
using CleanArcTemp.Application.Common.Models;
using CleanArcTemp.Domain.Entities;
using CleanArcTemp.Infrastructure.Identity;
using CleanArcTemp.Infrastructure.LogManager;
using CleanArcTemp.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CleanArcTemp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration,
        IConfigurationRoot authenticationSchemesConfiguration
        )
    {


        services.AddSingleton<ILogger, SerilogLogger>();


        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ICleanArcTempDbContext, CleanArcTempDbContext>(options =>
                options.UseInMemoryDatabase("StateSetInMemory"));
        }
        else
        {
            services.AddDbContext<CleanArcTempDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        services.AddScoped<ICleanArcTempDbContext>(provider => provider.GetRequiredService<CleanArcTempDbContext>());

        services
            .AddDefaultIdentity<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            }
            )
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<CleanArcTempDbContext>();


        services.AddIdentityServer()
            .AddApiAuthorization<User, CleanArcTempDbContext>();



        services.AddTransient<IIdentityService, IdentityService>();


        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })

            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!))
                };
            });



        var rollPolicyList = authenticationSchemesConfiguration
            .GetSection("RollPolicy").Get<List<RollPolicy>>()!.ToList();


        services.AddAuthorization(options =>
        {
            foreach (var rollPolicy in rollPolicyList)
            {
                options.AddPolicy(rollPolicy.Policy, policy => policy.RequireRole(rollPolicy.Roll));
            }
        });


        return services;
    }
}

