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
                    ParserSup2Mes parserSup2Mes = new ParserSup2Mes(dbContext);
                    parserSup2Mes.CheckSup2MesFolder();

                    ParserMes2Sup parserXMLMes = new ParserMes2Sup(dbContext);
                    parserXMLMes.CheckMes2SupFolder();

                    Pinger.PingAllStands(dbContext.stands.Where(k => k.IpAdress != null).ToList());

                    ParserJSON parser = new ParserJSON();
                    parser.AddAllJsonFiles(dbContext);

                    

                    ActualizeFilesInStand actualizeFilesInStand = new ActualizeFilesInStand();
                    actualizeFilesInStand.CheckFolderWithUnsendingFiles(dbContext);
                }                
            }

            catch (Exception ex)
            {
                LoggerTXT.LogError("Job error!" + "\n\n" + ex.ToString());
            }
            
        }
    }
}
