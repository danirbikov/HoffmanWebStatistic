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

                formationAndSendTranslateFile();
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

            formationAndSendTranslateFile();

            return RedirectToAction("MainMenu");
            
        }

        [HttpPost]
        public IActionResult UpdateAndSave([FromBody] Dictionary<string, string> inputData)
        {

            
            foreach (var dictionaryElement in inputData)
            {
                _translateRepository.AddOrEdit(new Translate() { EngVariant = dictionaryElement.Key, RusVariant = dictionaryElement.Value });
            }

            formationAndSendTranslateFile();

            return Ok();
        }

        public void formationAndSendTranslateFile()
        {
            TranslatesXMLFile translatesXMLFile = new TranslatesXMLFile();
            translatesXMLFile.FormationXMLFileForStands(_translateRepository.GetAll());

            StandOperationOLD interactionStand = new StandOperationOLD(_standRepository, _translatePathRepository, _sendingStatusLogRepository);
            interactionStand.SendFileOnStands("Translate", _usersRepository.GetUserByName(HttpContext.User.Identity.Name).Id);
        }

    }

}

