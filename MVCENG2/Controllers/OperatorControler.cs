using Microsoft.AspNetCore.Mvc;
using HoffmanWebstatistic.Interfaces;
using HoffmanWebstatistic.Models.General;
using HoffmanWebstatistic.Repository;
using Microsoft.AspNetCore.Authorization;
using HoffmanWebstatistic.Services;
using HoffmanWebstatistic.Models.ViewModel;
using HoffmanWebstatistic.Models.Hoffman;

namespace HoffmanWebstatistic.Controllers
{
    [Authorize(Roles = "sa, admin")]
    public class OperatorController : Controller
    {
        private readonly StandRepository _standRepository;
        private readonly OperatorsRepository _operatorsRepository;
        private readonly UsersRepository _usersRepository;
        private readonly SendingStatusLogRepository _sendingStatusLogRepository;

        public OperatorController(StandRepository standRepository, JsonHeadersRepository jsonHeadersRepository, OperatorsRepository operatorsRepository, UsersRepository usersRepository, SendingStatusLogRepository sendingStatusLogRepository)
        {
            _standRepository = standRepository;
            _operatorsRepository = operatorsRepository;
            _usersRepository = usersRepository;
            _sendingStatusLogRepository = sendingStatusLogRepository;
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

            InteractionStand interactionStand = new InteractionStand(_standRepository, null, _sendingStatusLogRepository);
            interactionStand.SendFileOnStands("Operator", _usersRepository.GetUserByName(HttpContext.User.Identity.Name).Id);

        }
    }

}

