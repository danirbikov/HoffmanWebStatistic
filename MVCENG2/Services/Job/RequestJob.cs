﻿using HoffmanWebstatistic.Data;
using Microsoft.EntityFrameworkCore;
using Quartz;
using ServicesWebAPI.Services;

namespace HoffmanWebstatistic.Services.Job
{
    [DisallowConcurrentExecution]
    public class RequestJob : IJob
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RequestJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    Pinger.PingAllStands(dbContext.stands.Where(k => k.IpAdress != null).ToList());

                    ParserJSON parser = new ParserJSON();

                    parser.AddAllJsonFiles(dbContext);
                }                
            }

            catch (Exception ex)
            {
                LoggerTXT.LogError("Job error!" + "\n\n" + ex.ToString());
            }
            
        }
    }
}