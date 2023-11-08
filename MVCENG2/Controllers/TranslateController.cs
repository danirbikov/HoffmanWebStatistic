using Microsoft.AspNetCore.Mvc;
using HoffmanWebstatistic.Repository;
using Microsoft.AspNetCore.Authorization;
using HoffmanWebstatistic.Models.Hoffman;
using HoffmanWebstatistic.Models.SerializerModels;
using System.Text.Json;

using System.Xml.Serialization;
using System.Linq.Expressions;
using ServicesWebAPI.Services;
using HoffmanWebstatistic.Services.FormationFile;
using HoffmanWebstatistic.Services.InteractionStand;

namespace HoffmanWebstatistic.Controllers
{
    [Authorize(Roles = "sa, admin")]
    public class TranslateController : Controller
    {
        private readonly TranslateRepository _translateRepository;
        private readonly StandRepository _standRepository;
        private readonly TranslatePathRepository _translatePathRepository;
        private readonly SendingStatusLogRepository _sendingStatusLogRepository;
        private readonly UsersRepository _usersRepository;

        public TranslateController(TranslateRepository translateRepository, StandRepository standRepository, TranslatePathRepository translatePathRepository,SendingStatusLogRepository sendingStatusLogRepository, UsersRepository usersRepository )
        {
            _translateRepository = translateRepository;
            _standRepository = standRepository;
            _translatePathRepository = translatePathRepository;
            _sendingStatusLogRepository = sendingStatusLogRepository;
            _usersRepository = usersRepository;
        }

        public async Task<IActionResult> MainMenu()
        {
            ViewData["standList"] = _translatePathRepository.GetAllWithInclude().Select(k => k.Stand.StandName).ToList();
            var allTranslates = _translateRepository.GetAll();

            return View(allTranslates);
        }


        [HttpPost]
        public async Task<IActionResult> ParseTranslationFile(IFormFile file)
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    ms.Seek(0, SeekOrigin.Begin);

                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(Entries));
                    Entries deserializeTranslationObject = (Entries)xmlSerializer.Deserialize(ms);

                    foreach (var translate in deserializeTranslationObject.E)
                    {
                        _translateRepository.Add(new Translate() { EngVariant = translate.key, RusVariant = translate.value });
                    }
                }
            }

            catch (Exception ex)
            {
                LoggerTXT.LogError(ex.Message);
            }            

            return RedirectToAction("MainMenu");
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteTranslate([FromBody] string translateEngName)
        {

            _translateRepository.Delete(translateEngName);

            return RedirectToAction("MainMenu");
            
        }

        [HttpPost]
        public IActionResult UpdateAndSave([FromBody] Dictionary<string, string> inputData)
        {           
            foreach (var dictionaryElement in inputData)
            {
                _translateRepository.AddOrEdit(new Translate() { EngVariant = dictionaryElement.Key, RusVariant = dictionaryElement.Value });
            }

            return Ok();
        }


        [HttpPost]
        public ActionResult SendFileOnStand([FromBody] string standName)
        {

            Stand stand = _standRepository.GetStandbyName(standName);

            TranslatesPath translatePath = _translatePathRepository.GetTranslatePathByStandID(stand.Id);
            int userId = _usersRepository.GetUserByName(HttpContext.User.Identity.Name).Id;

            TranslateOperation translateOperation = new TranslateOperation();
            SendingStatusLog sendingStatusLog = translateOperation.SendTranslateFileOnStands(stand, translatePath, userId);

            _sendingStatusLogRepository.AddOrUpdate(sendingStatusLog);

            if (sendingStatusLog.Status.ToUpper() == "OK")
            {
                return Ok(new { status = "success", stand = stand.StandName });
            }
            else
            {
                return BadRequest(new { status = "Error in server!!!! " + sendingStatusLog.ErrorMessage, stand = stand.StandName });
            }
        }

        [HttpPost]
        public ActionResult FormationFile()
        {
            try
            {
                TranslatesXMLFile translatesXMLFile = new TranslatesXMLFile();
                translatesXMLFile.FormationXMLFileForStands(_translateRepository.GetAll());
                return Ok(new { status = "success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "Error in server!!!! " + ex.Message });
            }

        }

    }

}

