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

        public OperatorController(StandRepository standRepository, JsonHeadersRepository jsonHeadersRepository, OperatorsRepository operatorsRepository)
        {
            _standRepository = standRepository;
            _operatorsRepository = operatorsRepository;
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

                OperatorsXMLFile.FormationAndSendXMLFileForStands(_standRepository.GetAll().ToList(), _operatorsRepository.GetAll().ToList());
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
        
     
    }

}

