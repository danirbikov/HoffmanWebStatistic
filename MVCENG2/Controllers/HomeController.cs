using Microsoft.AspNetCore.Mvc;
using MVCENG2.Interfaces;
using MVCENG2.Models;
using MVCENG2.Repository;
using MVCENG2.Services;
using System.Diagnostics;


namespace MVCENG2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStandRepository _standRepository;
        private readonly IStandsStatisticRepository _statisticRepository;
        private readonly ITestReportRepository _testReportRepository;
        public HomeController(ILogger<HomeController> logger, IStandRepository standRepository, IStandsStatisticRepository statisticRepository, ITestReportRepository testReportRepository)
        {
            _standRepository = standRepository;
            _statisticRepository = statisticRepository;
            _testReportRepository = testReportRepository;
            _logger = logger;
            
        }

        public IActionResult Index()
        {


            //Task.Run(() => { Services(); });
            return View();
        }
        public async Task Services()
        {
            Pinger pinger = new Pinger(_standRepository);
            await pinger.PingAllStands();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}