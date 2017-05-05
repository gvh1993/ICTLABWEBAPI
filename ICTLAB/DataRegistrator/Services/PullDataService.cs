using System;
using DataRegistrator.Jobs;
using Quartz;
using Quartz.Impl;

namespace DataRegistrator.Services
{
    class PullDataService : IService
    {
        public void Start()
        {
            Console.WriteLine("PullDataService is starting...");

            // construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();

            //JOBS DEFINITION
            // define the job and tie it to our PulldataJob class
            IJobDetail job = JobBuilder.Create<PullDataJob>()
                .WithIdentity("pullDataJob", "group1")
                .Build();

            IJobDetail job2 = JobBuilder.Create<RefreshTargetApiLinksJob>()
                .WithIdentity("refreshTargetApLinksJob", "group1")
                .Build();

            //TRIGGER DEFINITION
            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity("pullDataTrigger", "group1")
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInMinutes(1)
                  .RepeatForever())
              .Build();

            ITrigger trigger2 = TriggerBuilder.Create()
              .WithIdentity("refreshTargetApLinksTrigger", "group1")
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInHours(1)
                  .RepeatForever())
              .Build();

            //START THE SCHEDULES
            sched.ScheduleJob(job, trigger);
            sched.ScheduleJob(job2, trigger2);
        }

        public void Stop()
        {
            Console.WriteLine("PullDataService is stopping...");
        }
    }
}
