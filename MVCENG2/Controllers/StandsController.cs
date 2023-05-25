using Microsoft.AspNetCore.Mvc;
using MVCENG2.Interfaces;
using MVCENG2.Models.General;
using MVCENG2.Repository;
using Microsoft.AspNetCore.Authorization;
using MVCENG2.Services;

namespace MVCENG2.Controllers
{
    [Authorize]
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

        //[Authorize(Roles = "sa")]
        public async Task<IActionResult> Index()
        {
            ViewData["UserName"] = HttpContext.User.Identity.Name;
            ViewData["UserRole"] = HttpContext.User.Claims.Select(k=>k.Value).ToList()[1];
            //ParserJSON parser_obj = new ParserJSON(_testJsonRepository,_standRepository,_operatorRepository,_jsonHeadersRepository,_jsonTestsRepository,_jsonValuesRepository);
            //parser_obj.ParsingJsonFiles();
            IEnumerable<Stand> stands = _standRepository.GetAll();

           //for (int i =0; i<stands.Count(); i++)
           //{
            //   stands.Skip(i).Take(1).First().IpAdress = Pinger.PingOneStand(stands.Skip(i).Take(1).First()).ToString();
           //}

            return View(stands);




        }
    }
}

