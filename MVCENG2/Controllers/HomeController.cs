using Microsoft.AspNetCore.Mvc;
using HoffmanWebstatistic.Repository;
using Microsoft.AspNetCore.Authorization;
using HoffmanWebstatistic.Models.ViewModel;
using Microsoft.VisualBasic;
using ServicesWebAPI.Services;
using HoffmanWebstatistic.Models.Hoffman;
using HoffmanWebstatistic.Services.Job;
using HoffmanWebstatistic.Services;
using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;

namespace HoffmanWebstatistic.Controllers
{
    [Authorize(Roles = "sa, admin, viewer")]
    public class HomeController : Controller
    {

        private readonly StandRepository _standRepository;
        private readonly JsonHeadersRepository _jsonHeadersRepository;
        private readonly ApplicationDbContext dbContext;

        public HomeController(ILogger<HomeController> logger,StandRepository standRepository, JsonHeadersRepository jsonHeadersRepository, ApplicationDbContext dbContext)
        {
            _standRepository = standRepository;
            _jsonHeadersRepository = jsonHeadersRepository;
            this.dbContext = dbContext;
        }
        
        public async Task<IActionResult> Index()
        {
            List<string> superAdminEmails = dbContext.users.Include(k => k.Role).Where(k => k.Role.RName == "sa").Select(k => k.ULogin).ToList();
            LoggerNLOG.LogFatalError("Parser", "Error in parser \n" + "TestError", superAdminEmails);

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

