using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MVCENG2.Data;
using MVCENG2.Interfaces;
using PagedList;
using MVCENG2.Services;
using MVCENG2.Models.Siemens;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcApp.Models;
using MVCENG2.Models.Hoffman;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static MVCENG2.Models.Enums.SortingEnum;
using System.Xml.Linq;
using MVCENG2.Repository;
using MVCENG2.ComfortModules;

namespace MVCENG2.Controllers
{
    public class HoffmanController : Controller
    {
        private readonly JsonHeadersRepository _jsonHeadersRepository;
        private readonly ApplicationDbContext _dbContext;

        public HoffmanController(JsonHeadersRepository jsonHeadersRepository, JsonTestsRepository jsonTestsRepository, ApplicationDbContext dbContext)
        {
            _jsonHeadersRepository = jsonHeadersRepository;
            _dbContext = dbContext;
        }

        

        public async Task<IActionResult> Detail(string standsIdentifier="Hoffman", string searchIdentifier = null,  int pageNumber=1, SortState sortOrder = SortState.VINAsc)
        {
            ViewData["UserName"] = HttpContext.User.Identity.Name;
            ViewData["UserRole"] = HttpContext.User.Claims.Select(k => k.Value).ToList()[1];
            ViewData["StandsIdentifier"] = standsIdentifier;
            ViewData["SearchIdentifier"] = searchIdentifier;
            ViewBag.ColumnNames = new List<string>() { "VIN", "OrderNumber", "StandName", "Operator", "Date" };
            IQueryable<ResultsJsonHeader> resultsJsonHeader;

            //Получение всех ResultsJsonHeader моделей по значению standsIdentifier
            if (searchIdentifier == null)
            {
                resultsJsonHeader = _jsonHeadersRepository.GetJsonHeadersByStandsIdentidier(standsIdentifier);
            }
            else
            {
                SearchInDB searchClass = new SearchInDB(_dbContext);
                
                resultsJsonHeader = searchClass.SearchInAllDB(searchIdentifier);
            }

            #region Initialize paginated list (BoilerPlate code) 
            
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
                default:
                    resultsJsonHeader = resultsJsonHeader.OrderBy(s => s.VIN);
                    break;
            }
            // пагинация
            int pageSize = 13;          
            var jsonHeaderIDs = resultsJsonHeader.Select(k => k.Id).ToList(); 
            var count = jsonHeaderIDs.Count();
            var itemsId = jsonHeaderIDs.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();


            // формируем модель представления
            
            IndexViewModel viewModel = new IndexViewModel(
                _jsonHeadersRepository.GetJsonHeadersById(itemsId),
                new PageViewModel(count, pageNumber, pageSize),
                new SortViewModel(sortOrder)
            );
            #endregion
            return View(viewModel);




            //return View(PaginatedList<Statistic>.CreatePage(statistics_val, pageNumber ?? 1, pageSize));
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
