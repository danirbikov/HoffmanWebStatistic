using Microsoft.AspNetCore.Mvc;
using MVCENG2.Interfaces;
using MVCENG2.Models;
using MVCENG2.Models.General;
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
        private readonly ITestJsonRepository _testJsonRepository;
        private readonly Pinger _pinger;
        public HomeController(Pinger pinger, ILogger<HomeController> logger, IStandRepository standRepository, IStandsStatisticRepository statisticRepository, ITestReportRepository testReportRepository, ITestJsonRepository testJsonRepository)
        {
            _standRepository = standRepository;
            _statisticRepository = statisticRepository;
            _testReportRepository = testReportRepository;
            _logger = logger;
            _pinger = pinger;
            _testJsonRepository = testJsonRepository;   
            
        }

        public async Task<IActionResult> Index()
        { 
            //ParserJSON parserJSON = new ParserJSON(_testJsonRepository);
            //parserJSON.TestParsingJsonFile();
            //await Services(); 
            return View();
        }
        public async Task Services()
        {
            await _pinger.PingAllStands();
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