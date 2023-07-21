using PingerWebAPI.Repository;
using PingerWebAPI.Services;
using Quartz;
using ServicesWebAPI.Services;


namespace ServicesWebAPI.Services
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
            ApplicationDbContext dbContext = new ApplicationDbContext();

            try
            {
                ParserJSON.AddAllJsonFiles(dbContext);
                //Pinger.PingAllStands(dbContext.stands.Where(k => k.IpAdress != null).ToList());

            }
            catch (Exception ex)
            {
                LoggerTXT.LogServices(ex.ToString() + "\n\n");
            }
            return null;
        }
    }
}
