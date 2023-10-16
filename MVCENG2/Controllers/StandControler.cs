using Microsoft.AspNetCore.Mvc;
using HoffmanWebstatistic.Interfaces;
using HoffmanWebstatistic.Repository;
using Microsoft.AspNetCore.Authorization;
using HoffmanWebstatistic.Services;
using HoffmanWebstatistic.Models.ViewModel;
using HoffmanWebstatistic.Models.Hoffman;

namespace HoffmanWebstatistic.Controllers
{
    [Authorize(Roles = "sa, admin")]
    public class StandController : Controller
    {
        private readonly StandRepository _standRepository;
        private readonly JsonHeadersRepository _jsonHeadersRepository;

        public StandController(StandRepository standRepository, JsonHeadersRepository jsonHeadersRepository)
        {
            _standRepository = standRepository;
            _jsonHeadersRepository = jsonHeadersRepository;
        }

        public async Task<IActionResult> MainMenu()
        {;
            List<StandsForAdminPanelView> standsForAdminPanel = new List<StandsForAdminPanelView>();

            bool pingResultStand = false;

            foreach (Stand stand in _standRepository.GetAll().Where(k => k.StandName != "UNKNOWN" && k.InactiveMark == "FALSE").OrderBy(k => k.StandName))
            {
                Pinger.standsPingResult.TryGetValue(stand.StandName, out pingResultStand);

                if (pingResultStand == null)
                {
                    pingResultStand = false;
                };

                standsForAdminPanel.Add(new StandsForAdminPanelView()
                {
                    stand = stand,
                    allTestsCount = DateFunctions.GetAllTestsCountForStand(stand, _jsonHeadersRepository),
                    pingResult = pingResultStand,
                    lastTestDate = DateFunctions.GetLastTestDateForStand(stand, _jsonHeadersRepository),

                });
            }

            return View(standsForAdminPanel);
        }

        [HttpPost]
        public async Task<IActionResult> AddStand(StandAddViewModel standModel)
        {
                      
            if (ModelState.ErrorCount<=10)
            {
                standModel.stand.InactiveMark = "FALSE";
                standModel.stand.OSVersionNavigationID = 1;

                if (!(standModel.QNX.IpAdress=="0.0.0.0" && standModel.QNX.DnsName == "none"))
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
                }
                _standRepository.Add(standModel.stand);
                
                return RedirectToAction("MainMenu");
            }

            ViewBag.StandTypeList = _standRepository.GetStandsTypeName().ToList();
            return View(new StandAddViewModel());
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


    }
}

