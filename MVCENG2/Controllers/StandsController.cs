using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MVCENG2.Data;
using MVCENG2.Interfaces;
using PagedList;
using MVCENG2.Services;
using MVCENG2.Models.General;
using MVCENG2.Repository;

namespace MVCENG2.Controllers
{
    public class StandsController : Controller
    {

        private readonly ITestJsonRepository _testJsonRepository;
        private readonly IStandRepository _standRepository;
        private readonly OperatorsRepository _operatorRepository;
        private readonly JsonHeadersRepository _jsonHeadersRepository;
        private readonly JsonTestsRepository _jsonTestsRepository;
        private readonly JsonValuesRepository _jsonValuesRepository;
        private ITestJsonRepository testJsonRepository;
        //private readonly DbContext _context;



        public StandsController(ITestJsonRepository testJsonRepository, IStandRepository standRepository, OperatorsRepository operatorRepository, JsonHeadersRepository jsonHeadersRepository, JsonTestsRepository jsonTestsRepository, JsonValuesRepository jsonValuesRepository)
        {
            _testJsonRepository = testJsonRepository;
            _standRepository = standRepository;
            _operatorRepository = operatorRepository;
            _jsonHeadersRepository = jsonHeadersRepository;
            _jsonTestsRepository = jsonTestsRepository;
            _jsonValuesRepository = jsonValuesRepository;
            //_context = context; 
        }
        public async Task<IActionResult> Index()
        {
            //ParserJSON parser_obj = new ParserJSON(_testJsonRepository,_standRepository,_operatorRepository,_jsonHeadersRepository,_jsonTestsRepository,_jsonValuesRepository);
            //parser_obj.ParsingJsonFiles();
            IEnumerable<Stand> stands = _standRepository.GetAll();


            return View(stands);




        }
    }
}
