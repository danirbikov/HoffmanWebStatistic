using Microsoft.AspNetCore.Mvc;
using MVCENG2.Interfaces;
using MVCENG2.Models.General;
using MVCENG2.Repository;
using Microsoft.AspNetCore.Authorization;
using MVCENG2.Services;
using MVCENG2.Models.ViewModel;
using MVCENG2.Models.Hoffman;

namespace MVCENG2.Controllers
{
    [Authorize(Roles = "sa, admin")]
    public class AdminController : Controller
    {
        private readonly IStandRepository _standRepository;
        private readonly JsonHeadersRepository _jsonHeadersRepository;
        private readonly OperatorsRepository _operatorsRepository;

        public AdminController(IStandRepository standRepository, JsonHeadersRepository jsonHeadersRepository, OperatorsRepository operatorsRepository)
        {
            _standRepository = standRepository;
            _jsonHeadersRepository = jsonHeadersRepository;
            _operatorsRepository = operatorsRepository;
        }

        
        public async Task<IActionResult> AdminPanel()
        {
            Dictionary<List<StandsForAdminPanelView>, List<Operator>> infoForAdminPanel = new Dictionary<List<StandsForAdminPanelView>, List<Operator>>();
            List<StandsForAdminPanelView> standsForAdminPanel = new List<StandsForAdminPanelView>();

            bool pingResultStand = false;
            Dictionary<string, bool> pingResultFromAPI = new Dictionary<string, bool>();

            try
            {
                pingResultFromAPI = WebAPIClient.GetPingResult();
            }
            catch
            {
                pingResultFromAPI.Add("WebApi not activated", false);
            }


            foreach (Stand stand in _standRepository.GetAll().Where(k => k.StandName!="UNKNOWN").OrderBy(k=>k.StandName))
            {
                pingResultFromAPI.TryGetValue(stand.StandName, out pingResultStand);

                if (pingResultStand == null)
                {
                    pingResultStand = false;
                };

                standsForAdminPanel.Add(new StandsForAdminPanelView()
                {
                    standName = stand.StandName,
                    allTestsCount = DateFunctions.GetAllTestsCountForStand(stand, _jsonHeadersRepository),
                    pingResult = pingResultStand,
                    lastTestDate = DateFunctions.GetLastTestDateForStand(stand, _jsonHeadersRepository),
                    
                });                    
            }
            infoForAdminPanel.Add(standsForAdminPanel, _operatorsRepository.GetAll().ToList());

            return View(infoForAdminPanel);                    
        }


        [HttpGet]
        public async Task<IActionResult> AddOperator()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOperator(Operator operatorModel)
        {
            if (ModelState.ErrorCount <= 1)
            {
                operatorModel.Created = DateTime.Now;
                operatorModel.InactiveMark = "FALSE";               
                _operatorsRepository.Add(operatorModel);

                OperatorsXMLFile.FormationAndSendXMLFileForStands(_standRepository.GetAll().ToList(), _operatorsRepository.GetAll().ToList());
                return RedirectToAction("AdminPanel");
            }
            return View();
                

        }

        [HttpPost]
        public async Task<IActionResult> AddStand(StandAddViewModel standModel)
        {
                      
            if (ModelState.ErrorCount<=10)
            {
                standModel.stand.InactiveMark = "FALSE";
                standModel.stand.OSVersionNavigationID = 1;

                if (standModel.QNX.IpAdress!="none" && standModel.QNX.DnsName != "none")
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
            
                return RedirectToAction("AdminPanel");
            }

            return View();           
        }

        [HttpGet]
        public async Task<IActionResult> AddStand()
        {

            return View();
        }
    }
}

