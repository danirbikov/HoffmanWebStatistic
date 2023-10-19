using Microsoft.AspNetCore.Mvc;
using HoffmanWebstatistic.Repository;
using Microsoft.AspNetCore.Authorization;
using HoffmanWebstatistic.Models.Hoffman;
using HoffmanWebstatistic.Services.FormationFile;
using HoffmanWebstatistic.Services.InteractionStand;

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

            return View(_operatorsRepository.GetAll().Where(k => k.InactiveMark == "FALSE").ToList());
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

                FormationAndSendOperatoFileToStand();

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

            FormationAndSendOperatoFileToStand();

            return RedirectToAction("MainMenu");
            
        }
        [HttpGet]
        public async Task<IActionResult> UnactiveOperator(int operatorID)
        {
            _operatorsRepository.UnactiveOperator(operatorID);

            FormationAndSendOperatoFileToStand();

            return RedirectToAction("MainMenu");
            
        }

        public void FormationAndSendOperatoFileToStand()
        {
            OperatorsXMLFile.FormationXMLFileForStands(_standRepository, _operatorsRepository.GetAll().ToList());

            StandOperation interactionStand = new StandOperation(_standRepository, _sendingStatusLogRepository, _operatorPathRepository);
            interactionStand.SendFileOnStands("Operator", _usersRepository.GetUserByName(HttpContext.User.Identity.Name).Id);

        }
    }

}

