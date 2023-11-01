using Microsoft.AspNetCore.Mvc;
using HoffmanWebstatistic.Repository;
using Microsoft.AspNetCore.Authorization;
using HoffmanWebstatistic.Models.ViewModel;
using Microsoft.VisualBasic;
using ServicesWebAPI.Services;
using HoffmanWebstatistic.Models.Hoffman;
using HoffmanWebstatistic.Services.Job;
using HoffmanWebstatistic.Services;

namespace HoffmanWebstatistic.Controllers
{
    [Authorize(Roles = "sa, admin")]
    public class HomeController : Controller
    {

        private readonly StandRepository _standRepository;
        private readonly JsonHeadersRepository _jsonHeadersRepository;

        public HomeController(ILogger<HomeController> logger,StandRepository standRepository, JsonHeadersRepository jsonHeadersRepository)
        {
            _standRepository = standRepository;
            _jsonHeadersRepository = jsonHeadersRepository;


        }
        
        public async Task<IActionResult> Index()
        {
            IEnumerable<Stand> stands = _standRepository.GetAll().Where(k => k.StandType != "QNX"); ;

            Dictionary<string, int> carsLastMonth = new Dictionary<string, int>();
            foreach (Stand stand in stands)
            {
                carsLastMonth.Add(stand.StandName, _jsonHeadersRepository.GetCarsCountLastmonth(stand.Id));           
            }

            //return RedirectToAction("AdminPanel", "Admin");

            return View(new StandsForView()
            {
                stands = stands,
                testsLastMonth = carsLastMonth,

                pingerDict = Pinger.standsPingResult

            }); 
        }
    }
}

