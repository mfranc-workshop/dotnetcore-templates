using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Impl;
using Quartz.Spi;
using Quartz.Simpl;
using Quartz;
using System;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore;
using RawRabbit;
using RawRabbit.Configuration;
using RawRabbit.vNext;
using NLog;

namespace MicroserviceCore
{
    public static class Ioc
    {
        public static void InitializeContainer(this IApplicationBuilder app, Container container, IHostingEnvironment env)
        {
            container.RegisterMvcControllers(app);

            container.RegisterSingleton(() =>
            {
                var sched = new StdSchedulerFactory().GetScheduler().Result;
                sched.JobFactory = new SimpleInjectiorJobFactory(container);
                return sched;
            });

            container.RegisterSingleton<IBusClient>(() => {
                var busConfig = new RawRabbitConfiguration
                    {
                        Username = "guest",
                        Password = "guest",
                        Port = 5672,
                        VirtualHost = "/",
                        Hostnames = { "localhost" }
                    };

                var client = BusClientFactory.CreateDefault(busConfig);
                return client;
            });

            container.RegisterSingleton<CoreRabbitService>();

            container.Verify();
        }
    }

    public class CoreRabbitService 
    {
        private ILogger _logger = LogManager.GetCurrentClassLogger();

        public CoreRabbitService(IBusClient client)
        {
            
        }
    }

    class SimpleInjectiorJobFactory : SimpleJobFactory
    {
        private readonly Container _container;

        public SimpleInjectiorJobFactory(Container container)
        {
            _container = container;
        }

        public override IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                return (IJob)_container.GetInstance(bundle.JobDetail.JobType);
            }
            catch (Exception ex)
            {
                throw new SchedulerException($"Problem while instantiating job '{bundle.JobDetail.Key}' from the NinjectJobFactory.", ex);
            }
        }
    }
}
