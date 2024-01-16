using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Threading;
using Volo.Abp.Users;

namespace HolwnEcomerce.BackgroundWorkers.MailCampaigns
{
    public class EmailMarrketingWorker : AsyncPeriodicBackgroundWorkerBase
    {
        public EmailMarrketingWorker(
                AbpAsyncTimer timer,
                IServiceScopeFactory serviceScopeFactory
            ) : base(
                timer,
                serviceScopeFactory)
        {
            Timer.Period = 5 * 1000; //5 seconds
        }

        protected async override Task DoWorkAsync(
            PeriodicBackgroundWorkerContext workerContext)
        {
            Logger.LogInformation("Starting: Setting status of inactive users...");

            ////Resolve dependencies
            //var userRepository = workerContext
            //    .ServiceProvider
            //    .GetRequiredService<IUserRepository>();

            ////Do the work
            //await userRepository.UpdateInactiveUserStatusesAsync();

            Logger.LogInformation("Completed: Setting status of inactive users...");
        }
    }
}
