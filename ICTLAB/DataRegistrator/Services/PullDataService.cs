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

            //JOB + Trigger
            IJobDetail fetchDataJob = JobBuilder.Create<PullDataJob>()
                .WithIdentity("pullDataJob", "group1")
                .Build();

            ITrigger fetchDataTrigger = TriggerBuilder.Create()
             .WithIdentity("pullDataTrigger", "group1")
             .StartNow()
             .WithSimpleSchedule(x => x
                 .WithIntervalInMinutes(5)
                 .RepeatForever())
             .Build();

            //START THE SCHEDULES
            sched.ScheduleJob(fetchDataJob, fetchDataTrigger);
        }

        public void Stop()
        {
            Console.WriteLine("PullDataService is stopping...");
        }
    }
}
