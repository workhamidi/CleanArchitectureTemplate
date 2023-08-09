using CleanArcTemp.Application.Common.Interfaces;
using CleanArcTemp.Presentation.Filters;
using CleanArcTemp.Presentation.Service;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace CleanArcTemp.Presentation;

public static class DependencyInjection
{

    public static IServiceCollection PresentationDependencyInjection(this IServiceCollection services)
    {

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        
        services.AddHttpContextAccessor();
        

        services.AddControllers(options =>
        {
            options.Filters.Add<ApiExceptionFilterAttribute>();
        });
        
        
        // configures FluentValidation to automatically perform server-side validation for your models
        services.AddFluentValidationAutoValidation();

        // for client side validation (jquery and so on)
        services.AddFluentValidationClientsideAdapters();


        // disables the default behavior of returning a 400 Bad Request response when the model state is invalid
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        return services;
    }
}
