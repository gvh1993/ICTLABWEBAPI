using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace DataRegistrator.Jobs
{
    class RefreshTargetApiLinksJob: IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("RefreshTargetApiLinksJob is executing...");
            //TODO go through all targetapilinks in the mongodb database and refresh the targetapilink list in the service for the pulldataJob
        }
    }
}
