using Microsoft.AspNetCore.Mvc;
using HoffmanWebstatistic.Repository;
using Microsoft.AspNetCore.Authorization;
using HoffmanWebstatistic.Models.ViewModel;
using HoffmanWebstatistic.Models.Hoffman;
using System.Drawing;
using System.Xml.Linq;
using System.Xml;
using System.Data.SqlTypes;
using System.Text;
using ServicesWebAPI.Services;
using HoffmanWebstatistic.Services.InteractionStand;

namespace HoffmanWebstatistic.Controllers
{
    [Authorize(Roles = "sa, admin")]
    public class DTCController : Controller
    {
        private readonly DTCContentRepository _dtcContentRepository;
        private readonly StandRepository _standRepository;
        private readonly UsersRepository _usersRepository;
        private readonly SendingStatusLogRepository _sendingStatusLogRepository;
        private readonly DTCPathRepository _dtcPathRepository;

        public DTCController(DTCContentRepository dtcContentRepository, StandRepository standRepository, UsersRepository usersRepository, SendingStatusLogRepository sendingStatusLogRepository, DTCPathRepository dtcPathRepository)
        {
            _standRepository = standRepository;
            _usersRepository = usersRepository;
            _sendingStatusLogRepository = sendingStatusLogRepository;
            _dtcPathRepository = dtcPathRepository;
            _dtcContentRepository = dtcContentRepository;
        }

        public async Task<IActionResult> MainMenu()
        {           
            return View(_dtcContentRepository.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> AddDTC()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> AddDTC(List<IFormFile> files)
        {
            foreach (var file in files)
            {
                try
                {
                    using (var streamReader = new StreamReader(file.OpenReadStream()))
                    {
                        var readedData = streamReader.ReadToEnd();

                        DtcContent dtc = new DtcContent { Fname = file.FileName, Fdata = readedData, Created = DateTime.Now };

                        _dtcContentRepository.Add(dtc);

                        int userId = _usersRepository.GetUserByName(HttpContext.User.Identity.Name).Id;

                        StandOperationOLD interactionStand = new StandOperationOLD(_standRepository, _dtcContentRepository, _sendingStatusLogRepository, _dtcPathRepository);
                        interactionStand.AddDTCForStands(dtc, userId);



                    }
                }
                catch (Exception ex)
                {
                    LoggerTXT.LogError("Log Error LOL "+ex.Message);
                    LoggerTXT.LogError("Log Error LOL 2 " + ex.InnerException);
                }

            }

            return RedirectToAction("MainMenu");
          
        }

        
        [HttpGet]
        public async Task<IActionResult> EditDTC(int dtcId)
        {
                      
            var dtcObject = _dtcContentRepository.GetDTCById(dtcId);
            
            return View(dtcObject);

        }

        [HttpPost]
        public async Task<IActionResult> EditDTC(string oldDTCName, string newDTCName, IFormFile file)
        {
            StandOperationOLD interactionStand = new StandOperationOLD(_standRepository, _dtcContentRepository, _sendingStatusLogRepository, _dtcPathRepository);
            DtcContent newDTC = new DtcContent();

            if (file == null) 
            {
                newDTC = _dtcContentRepository.EditDTC(oldDTCName, newDTCName, null);
            }
            else
            {
                using (var streamReader = new StreamReader(file.OpenReadStream()))
                {
                    
                    var readedData = streamReader.ReadToEnd();
                    newDTC = _dtcContentRepository.EditDTC(oldDTCName, newDTCName, readedData);
                    
                }
            }

            int userId = _usersRepository.GetUserByName(HttpContext.User.Identity.Name).Id;

            interactionStand.DeleteDTCFromStands(oldDTCName, userId);

            interactionStand.EditDTCFromStands(newDTC, userId);
                       
            return RedirectToAction("MainMenu");            
        }
        
        [HttpGet]

        public async Task<IActionResult> DeleteDTC(string dtcName)
        {
            int userId = _usersRepository.GetUserByName(HttpContext.User.Identity.Name).Id;

            StandOperationOLD interactionStand = new StandOperationOLD(_standRepository, _dtcContentRepository, _sendingStatusLogRepository, _dtcPathRepository);
            interactionStand.DeleteDTCFromStands(dtcName, userId);

            _dtcContentRepository.Delete(dtcName);

            return RedirectToAction("MainMenu");
            
        }
        
     
    }

}

