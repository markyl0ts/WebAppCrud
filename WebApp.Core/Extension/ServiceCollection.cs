using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Repository;
using WebApp.Core.Repository.Interface;
using WebApp.Core.Services;
using WebApp.Core.Services.Interface;

namespace WebApp.Core.Extension
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            //-- Add repository here
            services.AddSingleton<ICustomerRepository, CustomerRepository>();

            //-- Add services here
            services.AddSingleton<ICustomerService, CustomerService>();

            return services;
        }
    }
}
