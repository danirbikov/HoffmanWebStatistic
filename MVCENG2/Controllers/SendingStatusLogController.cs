using Microsoft.AspNetCore.Mvc;
using HoffmanWebstatistic.Interfaces;
using HoffmanWebstatistic.Models.General;
using HoffmanWebstatistic.Models.Hoffman;
using HoffmanWebstatistic.Repository;
using Microsoft.AspNetCore.Authorization;
using HoffmanWebstatistic.Services;
using HoffmanWebstatistic.Models.ViewModel;


namespace HoffmanWebstatistic.Controllers
{
   
    public class SendingStatusLogController : Controller
    {

        private readonly SendingStatusLogRepository _sendingStatusLog;

        public SendingStatusLogController(SendingStatusLogRepository sendingStatusLog)
        {
            _sendingStatusLog = sendingStatusLog;
        }

        public async Task<IActionResult> MainMenu()
        {            
            IEnumerable<SendingStatusLog> sendingStatusLogs = _sendingStatusLog.GetAllWithInclude() ;
            

            return View(new SendingStatusLogViewModel()
            {
                sendingStatusLog = sendingStatusLogs,
                pingerDict = Pinger.standsPingResult
            }); 
        }
    }
}

