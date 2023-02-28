using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MVCENG2.Data;
using MVCENG2.Interfaces;
using MVCENG2.Models;
using PagedList;
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

       
        public async Task<IActionResult> Detail(Statistic statistic, string sortOrder, string currentFilter, string searchString ,int? pageNumber)
        {            
            IEnumerable<Statistic> statistics_val = await _statisticRepository.GetAllElementsThatStand(statistic.ProductionNumber);

            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            ViewData["ProdNumSortParm"] = sortOrder == "ProdNum" ? "ProdNum_desc" : "ProdNum";
            ViewData["VINSortParm"] = sortOrder == "VIN" ? "VIN_desc" : "VIN";
            ViewData["TestStartSortParm"] = sortOrder == "TestStart" ? "TestStart_desc" : "TestStart";
            ViewData["TestEndSortParm"] = sortOrder == "TestEnd" ? "TestEnd_desc" : "TestEnd";
            ViewData["TotalDurationSortParm"] = sortOrder == "TotalDuration" ? "TotalDuration_desc" : "TotalDuration";
            ViewData["ResultSortParm"] = sortOrder == "Result" ? "Result_desc" : "Result";
            ViewData["NotOksSortParm"] = sortOrder == "NotOks" ? "NotOks_desc" : "NotOks";
            ViewData["ClientSortParm"] = sortOrder == "Client" ? "Client_desc" : "Client";
            ViewData["TestTypeSortParm"] = sortOrder == "TestType" ? "TestType_desc" : "TestType";

            if (!String.IsNullOrEmpty(searchString))
            {
                statistics_val = statistics_val.Where(s => 
                                           s.ProductionNumber==searchString
                                        || s.VIN== searchString
                                        || s.TestStart== searchString
                                        || s.TestEnd== searchString
                                        || s.TotalDuration== searchString
                                        || s.Result== searchString
                                        || s.NotOks== searchString
                                        || s.Client== searchString
                                        || s.TestType== searchString
                                       );
            }
            if (searchString!=null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            switch (sortOrder)
            {
                case "ProdNum":
                    statistics_val = statistics_val.OrderBy(s => s.ProductionNumber);
                    break;
                case "ProdNum_desc":
                    statistics_val = statistics_val.OrderByDescending(s => s.ProductionNumber);
                    break;
                case "VIN":
                    statistics_val = statistics_val.OrderBy(s => s.VIN);
                    break;
                case "VIN_desc":
                    statistics_val = statistics_val.OrderByDescending(s => s.VIN);
                    break;
                case "TestStart":
                    statistics_val = statistics_val.OrderBy(s => s.TestStart);
                    break;
                case "TestStart_desc":
                    statistics_val = statistics_val.OrderByDescending(s => s.TestStart);
                    break;
                case "TestEnd":
                    statistics_val = statistics_val.OrderBy(s => s.TestEnd);
                    break;
                case "TestEnd_desc":
                    statistics_val = statistics_val.OrderByDescending(s => s.TestEnd);
                    break;
                case "TotalDuration":
                    statistics_val = statistics_val.OrderBy(s => s.TotalDuration);
                    break;
                case "TotalDuration_desc":
                    statistics_val = statistics_val.OrderByDescending(s => s.TotalDuration);
                    break;
                case "Result":
                    statistics_val = statistics_val.OrderBy(s => s.Result);
                    break;
                case "Result_desc":
                    statistics_val = statistics_val.OrderByDescending(s => s.Result);
                    break;
                case "NotOks":
                    statistics_val = statistics_val.OrderBy(s => s.NotOks);
                    break;
                case "NotOks_desc":
                    statistics_val = statistics_val.OrderByDescending(s => s.NotOks);
                    break;
                case "Client":
                    statistics_val = statistics_val.OrderBy(s => s.Client);
                    break;
                case "Client_desc":
                    statistics_val = statistics_val.OrderByDescending(s => s.Client);
                    break;
                case "TestType":
                    statistics_val = statistics_val.OrderBy(s => s.TestType);
                    break;
                case "TestType_desc":
                    statistics_val = statistics_val.OrderByDescending(s => s.TestType);
                    break;
                default:
                    statistics_val = statistics_val.OrderBy(s => s.ProductionNumber);
                    break;

            }

            int pageSize = 14;
            return View(PaginatedList<Statistic>.CreatePage(statistics_val, pageNumber ?? 1, pageSize));
            //return View(statistics_val);




            //MVCENG2.ComfortModules.DateFunctions dateFunctions = new MVCENG2.ComfortModules.DateFunctions(_standRepository,_statisticRepository);
            //List<IEnumerable<Statistic>> statistics = new();
            //for (int i = 0; i < stands_statistic.Count(); i++)
            //{
            //    statistics.Add(dateFunctions.GetValuesTimeInterval());
            //}
            //TempData["Statistics"] = statistics;
           // return View(stands_statistic);
        }

        public async Task<IActionResult> TestReport(string sortOrder)
        {
            IEnumerable<TestReport> test_report = await _testReportRepository.GetAll();


            
            return View(test_report);
        }





    }
}
