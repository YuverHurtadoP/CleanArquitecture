using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.API
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
           
            return services;
        }
    }
}