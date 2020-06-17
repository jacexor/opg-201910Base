using Microsoft.Extensions.DependencyInjection;
using opg_201910_interview.Core.Services.Clients;
using opg_201910_interview.Core.Services.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace opg_201910_interview
{
    public static class IoCFactory
    {
        public static void MapInterfaces(IServiceCollection services)
        {
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IClientService, ClientService>();
        }
    }
}
