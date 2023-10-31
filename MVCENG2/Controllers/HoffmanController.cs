using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Data;
using PagedList;
using HoffmanWebstatistic.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HoffmanWebstatistic.Models.Hoffman;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static HoffmanWebstatistic.Models.Enums.SortingEnum;
using System.Xml.Linq;
using HoffmanWebstatistic.Repository;
using HoffmanWebstatistic.Models.ViewModel;
using Microsoft.VisualBasic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace HoffmanWebstatistic.Controllers
{
    [Authorize(Roles = "sa, admin")]
    public class HoffmanController : Controller
    {
        private readonly JsonHeadersRepository _jsonHeadersRepository;

        public HoffmanController(JsonHeadersRepository jsonHeadersRepository, JsonTestsRepository jsonTestsRepository, ApplicationDbContext dbContext)
        {
            _jsonHeadersRepository = jsonHeadersRepository;
        }

        public IEnumerable<ResultsJsonHeader> InitializePageValue(string standsIdentifier, string searchIdentifier, int pageNumber, SortState sortOrder)
        {           
            IQueryable<ResultsJsonHeader> resultsJsonHeader;

            //Получение всех ResultsJsonHeader моделей по значению standsIdentifier
            if (searchIdentifier == null)
            {
                resultsJsonHeader = _jsonHeadersRepository.GetJsonHeadersByStandsIdentidier(standsIdentifier);
            }
            else
            {
                ViewData["StandsIdentifier"] = "Search: "+searchIdentifier;
                resultsJsonHeader = _jsonHeadersRepository.SearchInAllDB(searchIdentifier);
            }
          
            // сортировка
            switch (sortOrder)
            {
                case SortState.VINDesc:
                    resultsJsonHeader = resultsJsonHeader.OrderByDescending(s => s.VIN);
                    break;
                case SortState.OrderNumberAsc:
                    resultsJsonHeader = resultsJsonHeader.OrderBy(s => s.Ordernum);
                    break;
                case SortState.OrderNumberDesc:
                    resultsJsonHeader = resultsJsonHeader.OrderByDescending(s => s.Ordernum);
                    break;
                case SortState.StandNameAsc:
                    resultsJsonHeader = resultsJsonHeader.OrderBy(s => s.Stand.StandName);
                    break;
                case SortState.StandNameDesc:
                    resultsJsonHeader = resultsJsonHeader.OrderByDescending(s => s.Stand.StandName);
                    break;
                case SortState.OperatorAsc:
                    resultsJsonHeader = resultsJsonHeader.OrderBy(s => s.Operator.OLogin);
                    break;
                case SortState.OperatorDesc:
                    resultsJsonHeader = resultsJsonHeader.OrderByDescending(s => s.Operator.OLogin);
                    break;
                case SortState.DateAsc:
                    resultsJsonHeader = resultsJsonHeader.OrderBy(s => s.Created);
                    break;
                case SortState.DateDesc:
                    resultsJsonHeader = resultsJsonHeader.OrderByDescending(s => s.Created);
                    break;
                case SortState.TNameAsc:
                    resultsJsonHeader = resultsJsonHeader.OrderBy(s => s.ResultsJsonTests.FirstOrDefault().TName);
                    break;
                case SortState.TNameDesc:
                    resultsJsonHeader = resultsJsonHeader.OrderByDescending(s => s.ResultsJsonTests.FirstOrDefault().TName);
                    break;
                case SortState.TSpecNameAsc:
                    resultsJsonHeader = resultsJsonHeader.OrderBy(s => s.ResultsJsonTests.FirstOrDefault().TSpecname);
                    break;
                case SortState.TSpecNameDesc:
                    resultsJsonHeader = resultsJsonHeader.OrderByDescending(s => s.ResultsJsonTests.FirstOrDefault().TSpecname);
                    break;

                default:
                    resultsJsonHeader = resultsJsonHeader.OrderBy(s => s.VIN);
                    break;
            }
       
            return resultsJsonHeader;
        }

        public async Task<IActionResult> VINsReport(string standsIdentifier = "Hoffman", string searchIdentifier = null, int pageNumber = 1, SortState sortOrder = SortState.DateDesc)
        {
            ViewData["StandsIdentifier"] = standsIdentifier;
            ViewData["SearchIdentifier"] = searchIdentifier;

            // пагинация
            int pageSize = 11;
            var jsonHeaderIDs = InitializePageValue(standsIdentifier,searchIdentifier,pageNumber,sortOrder).GroupBy(t => t.VIN).Select(t => t.FirstOrDefault()).Select(k => k.Id).ToList();
            var count = jsonHeaderIDs.Count();
            var itemsId = jsonHeaderIDs.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();


            ReportViewModel viewModel = new ReportViewModel(
                _jsonHeadersRepository.GetJsonHeadersById(itemsId),
                new PageViewModel(count,pageNumber,pageSize),
                new SortViewModel(sortOrder)
            );
            
            return View(viewModel);
        }

        public async Task<IActionResult> JSONReport(string standsIdentifier = "Hoffman", string VIN = null, string searchIdentifier = null, int pageNumber = 1, SortState sortOrder = SortState.DateDesc)
        {
            ViewData["StandsIdentifier"] = standsIdentifier;
            ViewData["SearchIdentifier"] = searchIdentifier;
            
            int pageSize = 11;
            
            var jsonHeaderIDs = InitializePageValue(standsIdentifier, searchIdentifier, pageNumber, sortOrder).Where(k => k.VIN == VIN).Select(k=>k.Id).ToList();
            var count = jsonHeaderIDs.Count();
            var itemsId = jsonHeaderIDs.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();


            ReportViewModel viewModel = new ReportViewModel(
                 _jsonHeadersRepository.GetJsonHeadersById(itemsId),
                 new PageViewModel(count, pageNumber, pageSize),
                 new SortViewModel(sortOrder)
             );

            return View(viewModel);
        }
     

        public async Task<IActionResult> TestReport(long jsonHeaderID)
        {
            return View(_jsonHeadersRepository.GetJsonHeadersById(jsonHeaderID));
        }


        public async Task<IActionResult> Search(long jsonHeaderID)
        {
            return View(_jsonHeadersRepository.GetJsonHeadersById(jsonHeaderID));
        }




    }
}
