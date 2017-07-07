using System;
using System.Linq;
using NLog;
using Quartz;
using System.Threading.Tasks;

namespace MicroserviceCore.Jobs
{
    public class CoreJob : IJob
    {
        private ILogger _logger = LogManager.GetCurrentClassLogger();

        public CoreJob()
        {
        }

        public Task Execute(IJobExecutionContext jobContext)
        {
            _logger.Info("CoreJob Executing");
            try
            {
                _logger.Info("CoreJob doing its magic");
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error when executing job");
                return Task.FromResult(false);
            }
        }
    }
}