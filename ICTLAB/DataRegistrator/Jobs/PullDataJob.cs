using System;
using Quartz;

namespace DataRegistrator.Jobs
{
    class PullDataJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("PullDataJob is executing...");
            //TODO go through all targetapilinks and pull the data en put it properly in the mongodb database
        }
    }
}
