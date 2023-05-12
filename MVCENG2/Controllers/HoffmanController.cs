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
    public class HoffmanController : Controller
    {

        private readonly IStandsStatisticRepository _statisticRepository;
        private readonly ITestReportRepository _testReportRepository;
        public HoffmanController(IStandsStatisticRepository statisticRepository, ITestReportRepository testReportRepository)
        {
            _statisticRepository = statisticRepository;
            _testReportRepository = testReportRepository;
        }

        public async Task<IActionResult> Detail(string standsIdentidier, string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["StandsIdentifier"] = standsIdentidier;

            IEnumerable<Statistic> statistics_val = _statisticRepository.GetAllElementsThatStand(standsIdentidier);

            #region Initialize paginated list (BoilerPlate code) 


            ViewBag.ColumnNames = new List<string>() {"StandName", "VIN", "OrderNumber", "Model", "Operator", "Date" }; ;

            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;

            foreach (string columnName in ViewBag.ColumnNames)
            {
                ViewData[columnName + "SortParam"] = sortOrder == columnName ? columnName + "_desc" : columnName;
            }
            int pageSize = 14;

            switch (sortOrder)
            {
                case "VIN":
                    statistics_val = statistics_val.OrderBy(s => s.VIN);
                    break;
                case "VIN_desc":
                    statistics_val = statistics_val.OrderByDescending(s => s.VIN);
                    break;

            }

            //ViewData["ProdNumSortParm"] = sortOrder == "ProdNum" ? "ProdNum_desc" : "ProdNum";

            #endregion

            if (!String.IsNullOrEmpty(searchString))
            {
                statistics_val = statistics_val.Where(s =>
                                           s.ProductionNumber == searchString
                                        || s.VIN == searchString
                                        || s.TestStart == searchString
                                        || s.TestEnd == searchString
                                        || s.TotalDuration == searchString
                                        || s.Result == searchString
                                        || s.NotOks == searchString
                                        || s.Client == searchString
                                        || s.TestType == searchString
                                       );
            }
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }




            return View(PaginatedList<Statistic>.CreatePage(statistics_val, pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> TestReport()
        {
            IEnumerable<TestReport> test_report = await _testReportRepository.GetAll();


            return View(test_report);
        }





    }
}
