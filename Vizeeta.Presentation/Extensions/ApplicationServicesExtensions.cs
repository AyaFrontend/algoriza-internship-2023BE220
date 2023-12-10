using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vizeeta.Presentation.Errors;
using Vizeeta.Presentation.Helper;
using Vizeeta.Repository.IRepository;
using Vizeeta.Repository.ISpecification;
using Vizeeta.Repository.Repository;
using Vizeeta.Repository.Specification;
using Vizeeta.Service.IService;
using Vizeeta.Service.IServices;
using Vizeeta.Service.Services;


namespace Vizeeta.Presentation.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperProfile));
            services.AddSingleton<IResponseCacheServices, ResponseCacheService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped( typeof(ISpecification<>), typeof(BaseSpecification<>));
            
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory =
                actionContext =>
                {
                    var errors = actionContext.ModelState.Where(m => m.Value.Errors.Count > 0)
                                  .SelectMany(m => m.Value.Errors)
                                  .Select(e => e.ErrorMessage).ToArray();
                    var responseMessage = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(responseMessage);
                };
            });

            return services;
        }
    }
}
