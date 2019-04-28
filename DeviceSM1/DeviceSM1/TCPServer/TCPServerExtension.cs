using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceSM1.TCPServer
{
    public static class TCPServerExtension
    {
        public static IServiceCollection AddTCPSocketManager(this IServiceCollection services)
        {
            services.AddSingleton<Server>();

            return services;
        }
    }
}
