using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MVCENG2.Data;
using MVCENG2.Interfaces;
using PagedList;
using MVCENG2.Services;
using MVCENG2.Models.Siemens;

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
       
        public async Task<IActionResult> Detail(string standsIdentidier, string sortOrder, string currentFilter ,int? pageNumber)
        {
            ViewData["StandsIdentifier"] = standsIdentidier;

            IEnumerable<Statistic> statistics_val = _statisticRepository.GetAllElementsThatStand(standsIdentidier);

            #region Initialize paginated list (BoilerPlate code) 


            ViewBag.ColumnNames = new List<string>() { "ProductionNumber", "VIN", "TestStart", "TestEnd", "TotalDuration", "Results", "NotOks", "Client", "TestType" }; ;

            ViewData["CurrentSort"] = sortOrder;

            foreach (string columnName in ViewBag.ColumnNames)
            {
                ViewData[columnName+"SortParam"] = sortOrder == columnName ? columnName+"_desc" : columnName; 
            }
            int pageSize = 14;

            switch (sortOrder)
            {

                case "ProductionNumber":
                    statistics_val = statistics_val.OrderBy(s => s.ProductionNumber);
                    break;
                case "ProductionNumber_desc":
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
                case "Results":
                    statistics_val = statistics_val.OrderBy(s => s.Result);
                    break;
                case "Results_desc":
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

            //ViewData["ProdNumSortParm"] = sortOrder == "ProdNum" ? "ProdNum_desc" : "ProdNum";

            #endregion


           
            

            return View(PaginatedList<Statistic>.CreatePage(statistics_val, pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> TestReport()
        {
            IEnumerable<TestReport> test_report = await _testReportRepository.GetAll();

            
            return View(test_report);
        }





    }
}
