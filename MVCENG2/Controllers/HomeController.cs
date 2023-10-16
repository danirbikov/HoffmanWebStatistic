using Microsoft.AspNetCore.Mvc;
using HoffmanWebstatistic.Interfaces;
using HoffmanWebstatistic.Repository;
using Microsoft.AspNetCore.Authorization;
using HoffmanWebstatistic.Services;
using HoffmanWebstatistic.Models.ViewModel;
using Microsoft.VisualBasic;
using ServicesWebAPI.Services;
using HoffmanWebstatistic.Models.Hoffman;

namespace HoffmanWebstatistic.Controllers
{

    public class HomeController : Controller
    {

        private readonly StandRepository _standRepository;
        private readonly JsonHeadersRepository _jsonHeadersRepository;

        public HomeController(ILogger<HomeController> logger,StandRepository standRepository, JsonHeadersRepository jsonHeadersRepository)
        {
            _standRepository = standRepository;
            _jsonHeadersRepository = jsonHeadersRepository;


        }
        [Authorize(Roles = "sa, admin")]
        public async Task<IActionResult> Index()
        {

            IEnumerable<Stand> stands = _standRepository.GetAll().Where(k => k.StandType != "QNX"); ;

            Dictionary<string, int> carsLastMonth = new Dictionary<string, int>();
            foreach (Stand stand in stands)
            {
                carsLastMonth.Add(stand.StandName, DateFunctions.GetCarsCountLastmonth(stand, _jsonHeadersRepository));           
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

