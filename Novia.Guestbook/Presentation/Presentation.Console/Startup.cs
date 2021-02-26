using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Novia.Guestbook.Configuration;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Novia.Guestbook.Presentation.Console
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            ServiceContainerConfigurator.ConfigureServices(configuration.GetConnectionString("DefaultConnection"), services);
        }
    }
}