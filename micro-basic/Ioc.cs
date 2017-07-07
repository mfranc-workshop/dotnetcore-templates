using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore;

namespace MicroserviceCore
{
    public static class Ioc
    {
        public static void InitializeContainer(this IApplicationBuilder app, Container container, IHostingEnvironment env)
        {
            container.RegisterMvcControllers(app);


            container.Verify();
        }
    }
}