using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using DataRegistrator.Models;

namespace DataRegistrator.Jobs
{
    [PersistJobDataAfterExecution]
    [DisallowConcurrentExecution]
    class RefreshTargetApiLinksJob: IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("RefreshTargetApiLinksJob is executing...");
            List<Home> homes = new List<Home>();

            //TODO go through all targetapilinks in the mongodb database and refresh the targetapilink list in the service for the pulldataJob
            MongoDB.MongoDBConnector connector = new MongoDB.MongoDBConnector();
            JobDataMap dataMap = context.MergedJobDataMap;

            int testInt = dataMap.GetInt("testInt");
            testInt += 1;
            Console.WriteLine(testInt);
            dataMap.Put("testInt", testInt);
            //dataMap["testInt"] = testInt;
            

            //iterate over collections/homes
            //iterate over the sensors per homeList of home objects
            //add the sensors to 
            
        }
    }
}
