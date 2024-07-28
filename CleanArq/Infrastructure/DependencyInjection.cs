using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplication.Data;
using Domain.Customers;
using Domain.Primitives;
using Infrastructure.Persistence;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this  IServiceCollection services, IConfiguration configuration){
            services.AddPersistence(configuration);

           
            return services;
        }

        private static IServiceCollection AddPersistence(this  IServiceCollection services, IConfiguration configuration){
            services.AddDbContext<AplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("conexionBD")));
            services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<AplicationDbContext>());
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AplicationDbContext>());
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            return services;

        }
    }
}