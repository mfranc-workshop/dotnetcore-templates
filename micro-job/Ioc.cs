using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Impl;
using Quartz.Spi;
using Quartz.Simpl;
using Quartz;
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

            container.RegisterSingleton(() =>
            {
                var sched = new StdSchedulerFactory().GetScheduler().Result;
                sched.JobFactory = new SimpleInjectiorJobFactory(container);
                return sched;
            });

            container.Verify();
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
