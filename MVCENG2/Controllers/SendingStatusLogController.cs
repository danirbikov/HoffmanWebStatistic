using Microsoft.AspNetCore.Mvc;
using HoffmanWebstatistic.Repository;
using Microsoft.AspNetCore.Authorization;
using HoffmanWebstatistic.Models.ViewModel;
using HoffmanWebstatistic.Services.Job;

namespace HoffmanWebstatistic.Controllers
{
    [Authorize(Roles = "sa, admin")]
    public class SendingStatusLogController : Controller
    {

        private readonly SendingStatusLogRepository _sendingStatusLog;

        public SendingStatusLogController(SendingStatusLogRepository sendingStatusLog)
        {
            _sendingStatusLog = sendingStatusLog;
        }

        [Authorize(Roles = "sa, admin")]
        public async Task<IActionResult> MainMenu()
        {            
            IEnumerable<SendingStatusLog> sendingStatusLogs = _sendingStatusLog.GetAllWithInclude();
            

            return View(new SendingStatusLogViewModel()
            {
                sendingStatusLog = sendingStatusLogs,
                pingerDict = Pinger.standsPingResult
            }); 
        }
    }
}

