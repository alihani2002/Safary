using Domain.Helpers;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safary.Repository
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddServicesRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailBodyBuilder, EmailBodyBuilder>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddTransient<IEmailSender, EmailSender>();
            

            return services;
        }
    }

}
