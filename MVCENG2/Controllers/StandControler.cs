using Microsoft.AspNetCore.Mvc;
using HoffmanWebstatistic.Repository;
using Microsoft.AspNetCore.Authorization;
using HoffmanWebstatistic.Models.ViewModel;
using HoffmanWebstatistic.Models.Hoffman;
using HoffmanWebstatistic.Services.Job;
using HoffmanWebstatistic.Services;
using System.Security.Claims;

namespace HoffmanWebstatistic.Controllers
{
    [Authorize(Roles = "sa")]
    public class StandController : Controller
    {
        private readonly StandRepository _standRepository;
        private readonly JsonHeadersRepository _jsonHeadersRepository;
        private readonly TranslatePathRepository _translatePathRepository;
        private readonly DTCPathRepository _dTCPathRepository;
        private readonly JsonPathRepository _jsonPathRepository;
        private readonly Mes2SupPathRepository _mes2SupPathRepository;
        private readonly OperatorPathRepository _operatorPathRepository;
        private readonly PicturePathRepository _picturePathRepository;
        private readonly Sup2MesPathRepository _sup2MesPathRepository;

        public StandController(StandRepository standRepository, JsonHeadersRepository jsonHeadersRepository, TranslatePathRepository translatePathRepository, DTCPathRepository dTCPathRepository, JsonPathRepository jsonPathRepository, Mes2SupPathRepository mes2SupPathRepository, OperatorPathRepository operatorPathRepository, PicturePathRepository picturePathRepository, Sup2MesPathRepository sup2MesPathRepository)
        {
            _standRepository = standRepository;
            _jsonHeadersRepository = jsonHeadersRepository;
            _translatePathRepository = translatePathRepository;
            _dTCPathRepository = dTCPathRepository;
            _jsonPathRepository = jsonPathRepository;
            _mes2SupPathRepository = mes2SupPathRepository;
            _operatorPathRepository = operatorPathRepository;
            _picturePathRepository = picturePathRepository;
            _sup2MesPathRepository = sup2MesPathRepository;
        }

        public async Task<IActionResult> MainMenu()
        {
            List<StandsForAdminPanelView> standsForAdminPanel = new List<StandsForAdminPanelView>();     
            bool pingResultStand = false;

            var stands = _standRepository.GetAll().Where(k => k.StandName != "UNKNOWN").OrderBy(k => k.StandName); ;
            if (User.IsInRole("sa"))
            {
                stands = _standRepository.GetAllWithInactive().Where(k => k.StandName != "UNKNOWN").OrderBy(k => k.StandName);
            }
        

            foreach (Stand stand in stands)
            {
                Pinger.standsPingResult.TryGetValue(stand.StandName, out pingResultStand);

                if (pingResultStand == null)
                {
                    pingResultStand = false;
                };

                standsForAdminPanel.Add(new StandsForAdminPanelView()
                {
                    stand = stand,
                    allTestsCount = _jsonHeadersRepository.GetAllTestsCountByStandId(stand.Id),
                    pingResult = pingResultStand,
                    lastTestDate = _jsonHeadersRepository.GetLastTestDateByStandId(stand.Id),

                });
            }

            return View(standsForAdminPanel);
        }

        [HttpPost]
        public async Task<IActionResult> AddStand(StandAddViewModel standModel)
        {


            if (ModelState.ErrorCount<=25)
            {
                standModel.stand.InactiveMark = "FALSE";
                standModel.stand.OSVersionNavigationID = 1;

                if (standModel.checkbox)
                {
                    standModel.QNX.StandName = standModel.stand.StandName + "_QNX";
                    standModel.QNX.StandNameDescription = standModel.stand.StandNameDescription;
                    standModel.QNX.WorkplaceMes = standModel.stand.WorkplaceMes;
                    standModel.QNX.Placement = standModel.stand.Placement;
                    standModel.QNX.OSVersionNavigationID = 2;
                    standModel.QNX.StandType = "QNX";
                    standModel.QNX.Project = standModel.stand.Project;
                    standModel.QNX.InactiveMark = standModel.stand.InactiveMark;

                    _standRepository.Add(standModel.QNX);
                    var standModelDBQNX = _standRepository.GetStandbyName(standModel.QNX.StandName);

                    standModel.dtcPath.Stand = standModelDBQNX;
                    standModel.jsonsPath.Stand = standModelDBQNX;
                    standModel.operatorsPath.Stand = standModelDBQNX;
                    standModel.mes2SupPath.Stand = standModelDBQNX;
                    standModel.picturesPath.Stand = standModelDBQNX;
                    standModel.sup2mesPath.Stand = standModelDBQNX;

                    _dTCPathRepository.Add(standModel.dtcPath);
                    _jsonPathRepository.Add(standModel.jsonsPath);
                    _mes2SupPathRepository.Add(standModel.mes2SupPath);
                    _operatorPathRepository.Add(standModel.operatorsPath);
                    _picturePathRepository.Add(standModel.picturesPath);
                    _sup2MesPathRepository.Add(standModel.sup2mesPath);
                    
                }

                _standRepository.Add(standModel.stand);
                var standModelDBWindows = _standRepository.GetStandbyName(standModel.stand.StandName);

                standModel.translatesPath.Stand = standModelDBWindows;
                _translatePathRepository.Add(standModel.translatesPath);
                
                return RedirectToAction("MainMenu");
            }

            ViewBag.StandTypeList = _standRepository.GetStandsTypeName().ToList();
            return View(standModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddStand()
        {
            ViewBag.StandTypeList = _standRepository.GetStandsTypeName().ToList();
            return View(new StandAddViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> EditStand(int standID)
        {
            ViewBag.StandTypeList = _standRepository.GetStandsTypeName().ToList();

            return View(new StandAddViewModel 
            {
                stand =  _standRepository.GetStandByID(standID),
            }
            );
        }
        [HttpPost]
        public async Task<IActionResult> EditStand(StandAddViewModel standObject)
        {
            _standRepository.EditStand(standObject.stand);

            return RedirectToAction("MainMenu");
        }

        [HttpGet]
        public async Task<IActionResult> UnactiveStand(int standID)
        {
            _standRepository.UnactiveStand(standID);
            return RedirectToAction("MainMenu");
        }

        [HttpGet]
        public async Task<IActionResult> InactiveStand(int standID)
        {
            _standRepository.InactiveStand(standID);
            return RedirectToAction("MainMenu");
        }
    }
}

