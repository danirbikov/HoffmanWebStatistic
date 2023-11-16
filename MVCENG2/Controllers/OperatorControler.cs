using Microsoft.AspNetCore.Mvc;
using HoffmanWebstatistic.Repository;
using Microsoft.AspNetCore.Authorization;
using HoffmanWebstatistic.Models.Hoffman;
using HoffmanWebstatistic.Services.InteractionStand;
using HoffmanWebstatistic.Models.ViewModel;
using ServicesWebAPI.Services;
using static HoffmanWebstatistic.Models.SerializerModels.JSONSerializeModel;
using HoffmanWebstatistic.Services.FormationFile;
using Microsoft.EntityFrameworkCore;

namespace HoffmanWebstatistic.Controllers
{
    [Authorize(Roles = "sa, admin")]
    public class OperatorController : Controller
    {
        private readonly StandRepository _standRepository;
        private readonly OperatorsRepository _operatorsRepository;
        private readonly UsersRepository _usersRepository;
        private readonly SendingStatusLogRepository _sendingStatusLogRepository;
        private readonly OperatorPathRepository _operatorPathRepository;

        public OperatorController(StandRepository standRepository, OperatorsRepository operatorsRepository, UsersRepository usersRepository, SendingStatusLogRepository sendingStatusLogRepository, OperatorPathRepository operatorPathRepository)
        {
            _standRepository = standRepository;
            _operatorsRepository = operatorsRepository;
            _usersRepository = usersRepository;
            _sendingStatusLogRepository = sendingStatusLogRepository;
            _operatorPathRepository = operatorPathRepository;
        }


        public async Task<IActionResult> MainMenu()
        {
            ViewData["standList"] = _operatorPathRepository.GetAllWithInclude().Select(k => k.Stand.StandName).ToList();
            return View(_operatorsRepository.GetAll());
                
        }

        [HttpGet]
        public async Task<IActionResult> AddOperator()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOperator(Operator operatorModel)
        {
            if (_operatorsRepository.OperatorAnyByLogin(operatorModel.OLogin))
            {
                ModelState.AddModelError("OLogin", "Оператор с таким логином уже существует");
                return View(operatorModel);
            }

            if (ModelState.ErrorCount <= 1)
            {
                operatorModel.Created = DateTime.Now;
                operatorModel.InactiveMark = "FALSE";               
                _operatorsRepository.Add(operatorModel);

                return RedirectToAction("MainMenu");
            }
            return View();               
        }


        [HttpGet]
        public async Task<IActionResult> EditOperator(int operatorID)
        {
            
            return View(_operatorsRepository.GetOperatorByID(operatorID));

        }

        [HttpPost]
        public async Task<IActionResult> EditOperator(Operator operatorObject)
        {

            _operatorsRepository.EditOperator(operatorObject);

            return RedirectToAction("MainMenu");
            
        }
        [HttpGet]
        public async Task<IActionResult> UnactiveOperator(int operatorID)
        {
            _operatorsRepository.UnactiveOperator(operatorID);

            return RedirectToAction("MainMenu");
            
        }



        [HttpPost]
        public ActionResult FormationFile()
        {
            try
            {
                OperatorsXMLFile.FormationXMLFileForStands(_standRepository.GetAll(), _operatorsRepository.GetAll());
                return Ok(new { status = "success"});
            } 
            catch (Exception ex)
            {
                return BadRequest(new { status = "Error in server!!!! " + ex.Message });
            }
           
        }

        [HttpPost]
        public ActionResult SendFileOnStand([FromBody] string standName)
        {

            Stand stand = _standRepository.GetStandbyName(standName);

            OperatorsPath operatorPath = _operatorPathRepository.GetOperatorPathByStandID(stand.Id);
            int userId = _usersRepository.GetUserByName(HttpContext.User.Identity.Name).Id;

            OperatorOperation operatorOperation = new OperatorOperation();
            SendingStatusLog sendingStatusLog = operatorOperation.SendOperatorFileOnStand(operatorPath, stand, userId);

            _sendingStatusLogRepository.AddOrUpdate(sendingStatusLog);

            if (sendingStatusLog.Status.ToUpper()=="OK")
            {
                return Ok(new { status = "success", stand = stand.StandName });
            }
            else
            {
                return BadRequest(new { status = "Error in server!!!! " + sendingStatusLog.ErrorMessage, stand = stand.StandName });
            }                     
        }
    }
}

