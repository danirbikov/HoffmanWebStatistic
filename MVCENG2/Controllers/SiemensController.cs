using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCENG2.Data;
using MVCENG2.Interfaces;
using MVCENG2.Models;
using MVCENG2.Services;

namespace MVCENG2.Controllers
{
    public class SiemensController : Controller
    {
        
        private readonly IStandRepository _standRepository;
        private readonly IStandsStatisticRepository _statisticRepository;
        private readonly ITestReportRepository _testReportRepository;
        public SiemensController(IStandRepository standRepository, IStandsStatisticRepository statisticRepository, ITestReportRepository testReportRepository)
        {
            _standRepository = standRepository;
            _statisticRepository = statisticRepository;
            _testReportRepository = testReportRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Stand> stands = await _standRepository.GetAll();
            

            return View(stands);
        }

       
        public async Task<IActionResult> Detail(Statistic statistic)
        {            
            IEnumerable<Statistic> stands_statistic = await _statisticRepository.GetAllElementsThatStand(statistic.ProductionNumber);


            //MVCENG2.ComfortModules.DateFunctions dateFunctions = new MVCENG2.ComfortModules.DateFunctions(_standRepository,_statisticRepository);
            //List<IEnumerable<Statistic>> statistics = new();
            //for (int i = 0; i < stands_statistic.Count(); i++)
            //{
            //    statistics.Add(dateFunctions.GetValuesTimeInterval());
            //}
            //TempData["Statistics"] = statistics;
            return View(stands_statistic);
        }

        public async Task<IActionResult> TestReport(TestReport testReport)
        {
            IEnumerable<TestReport> test_report = await _testReportRepository.GetAll();
            return View(test_report);
        }





    }
}
