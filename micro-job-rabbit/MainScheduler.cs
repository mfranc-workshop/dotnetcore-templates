using System;
using System.ComponentModel;
using System.Net.NetworkInformation;
using NLog;
using Quartz;
using Quartz.Impl;
using MicroserviceCore.Jobs;
using SimpleInjector;

namespace MicroserviceCore 
{
    public static class MainScheduler
    {
        private static ILogger _logger = LogManager.GetCurrentClassLogger();
        private static IScheduler _scheduler;

        public static void Start(Container container)
        {
            try
            {
                _scheduler = container.GetInstance<IScheduler>();
                _scheduler.Start();

                var triggerTransfer = TriggerBuilder.Create()
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(60)
                        .RepeatForever())
                    .Build();

                var transferJob = JobBuilder.Create<CoreJob>().Build();
                _scheduler.ScheduleJob(transferJob, triggerTransfer);

                _logger.Info("Transfer Job Was scheduled and started - every 60s");
            }
            catch (SchedulerException se)
            {
                _logger.Error(se, "There was an exception when starting scheduler");
                Console.WriteLine(se);
            }
        }

        public static void Stop()
        {
           _scheduler.Shutdown();
        }
    }
}