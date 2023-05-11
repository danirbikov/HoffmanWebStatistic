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
      
        private readonly IStandsStatisticRepository _statisticRepository;
        private readonly ITestReportRepository _testReportRepository;
        public SiemensController(IStandsStatisticRepository statisticRepository, ITestReportRepository testReportRepository)
        {
            _statisticRepository = statisticRepository;
            _testReportRepository = testReportRepository;
        }
       
        public async Task<IActionResult> Detail(string standsIdentidier, string sortOrder, string currentFilter, string searchString ,int? pageNumber)
        {            
            IEnumerable<Statistic> statistics_val = _statisticRepository.GetAllElementsThatStand(standsIdentidier);
            
            #region Initialize paginated list (BoilerPlate code) 

            int pageSize = 14;
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
            #endregion

            


            return View(PaginatedList<Statistic>.CreatePage(statistics_val, pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> TestReport(string sortOrder)
        {
            IEnumerable<TestReport> test_report = await _testReportRepository.GetAll();

            var a = ViewData["CurrentSort"];
            
            return View(test_report);
        }





    }
}
