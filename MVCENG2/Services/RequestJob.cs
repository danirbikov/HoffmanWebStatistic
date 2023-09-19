﻿using HoffmanWebstatistic.Data;
using Quartz;
using ServicesWebAPI.Services;

namespace HoffmanWebstatistic.Services
{
    [DisallowConcurrentExecution]
    public class RequestJob : IJob
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RequestJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task Execute(IJobExecutionContext context)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                try
                {
                    Pinger.PingAllStands(dbContext.stands.Where(k => k.IpAdress != null).ToList());

                    ParserJSON parser = new ParserJSON();
                    parser.AddAllJsonFiles(dbContext);                                        
                }

                catch (Exception ex)
                {
                    LoggerTXT.LogServices(ex.ToString() + "\n\n");
                }
                return null;
            }
        }
    }
}