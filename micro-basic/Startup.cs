﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using NLog.Extensions.Logging;
using NLog.Web;

namespace MicroserviceCore
{
    public class Startup
    {
        private Container _container = new Container();

        public Startup(IHostingEnvironment env, ILogger<Startup> logger)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            env.ConfigureNLog("NLog.config");

            logger.LogInformation("Service started");
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton<IControllerActivator>((x) => {
                return new SimpleInjectorControllerActivator(_container);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifeTime)
        {
            loggerFactory.AddNLog();

            app.InitializeContainer(_container, env);

            app.UseDeveloperExceptionPage();

            app.UseMvc();

            appLifeTime.ApplicationStopping.Register(() => _container.Dispose());
        }
    }
}
