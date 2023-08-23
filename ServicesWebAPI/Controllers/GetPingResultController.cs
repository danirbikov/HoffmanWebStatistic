using Microsoft.AspNetCore.Mvc;
using PingerAPI.Models.General;
using PingerWebAPI.Repository;
using PingerWebAPI.Services;
using ServicesWebAPI.Services;

namespace ServicesWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetPingResultController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public GetPingResultController(ApplicationDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public List<Stand> GetPingResults()
        {
            try
            {
                LoggerTXT.LogPinger("GO!!!");

                return dbContext.stands.ToList();
                
                //return Pinger.standsPingResult;
            }

            catch
            {
                return null;
            }

        }
    }
}