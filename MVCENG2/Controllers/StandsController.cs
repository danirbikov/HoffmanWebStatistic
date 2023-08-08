using Microsoft.AspNetCore.Mvc;
using MVCENG2.Interfaces;
using MVCENG2.Models.General;
using MVCENG2.Repository;
using Microsoft.AspNetCore.Authorization;
using MVCENG2.Services;
using MVCENG2.Models.ViewModel;
using Microsoft.VisualBasic;

namespace MVCENG2.Controllers
{
   
    public class StandsController : Controller
    {

        private readonly IStandRepository _standRepository;
        private readonly JsonHeadersRepository _jsonHeadersRepository;

        public StandsController(IStandRepository standRepository, JsonHeadersRepository jsonHeadersRepository)
        {
            _standRepository = standRepository;
            _jsonHeadersRepository = jsonHeadersRepository;
        }

        //[Authorize(Roles = "sa")]
        public async Task<IActionResult> Index()
        {           
            
            IEnumerable<Stand> stands = _standRepository.GetAll();

            Dictionary<string, int> carsLastMonth = new Dictionary<string, int>();
            foreach (Stand stand in stands)
            {
                //carsLastMonth.Add(stand.StandName, DateFunctions.GetCarsCountLastmonth(stand, _jsonHeadersRepository));           
            }

            //return RedirectToAction("AdminPanel", "Admin");
            
            return View(new StandsForView()
            {
                stands = stands,
                testsLastMonth = carsLastMonth,
#if RELEASE
                pingerDict = WebAPIClient.GetPingResult()
#else 
                pingerDict = null
#endif
            }); 
        }
    }
}

