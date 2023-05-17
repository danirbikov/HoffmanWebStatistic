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

namespace MVCENG2.Controllers
{
    public class HoffmanController : Controller
    {
        private readonly JsonHeadersRepository _jsonHeadersRepository;

        public HoffmanController(JsonHeadersRepository jsonHeadersRepository)
        {
            _jsonHeadersRepository = jsonHeadersRepository;
        }

        public async Task<IActionResult> Detail(string standsIdentifier,  int pageNumber=1, SortState sortOrder = SortState.VINAsc)
        {
            ViewData["StandsIdentifier"] = standsIdentifier;
            ViewBag.ColumnNames = new List<string>() { "VIN", "OrderNumber", "StandName", "Operator", "Date" };

            //Добавление в модуль ResultsJsonHeader модели Cтенда
            IQueryable<ResultsJsonHeader> resultsJsonHeader = _jsonHeadersRepository.Include();

            //Получение всех ResultsJsonHeader моделей по значению standsIdentifier
            resultsJsonHeader = _jsonHeadersRepository.GetJsonHeadersByStandsIdentidier(standsIdentifier, resultsJsonHeader);

            #region Initialize paginated list (BoilerPlate code) 
            int pageSize = 13;
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
            var count = resultsJsonHeader.Count();
            var items = resultsJsonHeader.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            // формируем модель представления
            IndexViewModel viewModel = new IndexViewModel(
                items,
                new PageViewModel(count, pageNumber, pageSize),
                new SortViewModel(sortOrder)
            );
            #endregion
            return View(viewModel);




            //return View(PaginatedList<Statistic>.CreatePage(statistics_val, pageNumber ?? 1, pageSize));
        }

        //public async Task<IActionResult> TestReport()
        //{
         //   IEnumerable<TestReport> test_report = await _testReportRepository.GetAll();
        

         //   return View(test_report);
       // }





    }
}
