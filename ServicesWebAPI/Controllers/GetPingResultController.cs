using Microsoft.AspNetCore.Mvc;

using PingerWebAPI.Services;


namespace ServicesWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetPingResultController : ControllerBase
    {

        [HttpGet]
        public Dictionary<string, bool> GetPingResults()
        {
            try
            {
                return Pinger.standsPingResult;
            }

            catch
            {
                return null;
            }

        }
    }
}