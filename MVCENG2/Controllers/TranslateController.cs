using Microsoft.AspNetCore.Mvc;
using HoffmanWebstatistic.Repository;
using Microsoft.AspNetCore.Authorization;
using HoffmanWebstatistic.Models.Hoffman;
using HoffmanWebstatistic.Models.SerializerModels;
using System.Text.Json;

using System.Xml.Serialization;
using System.Linq.Expressions;
using ServicesWebAPI.Services;

namespace HoffmanWebstatistic.Controllers
{
    [Authorize(Roles = "sa, admin")]
    public class TranslateController : Controller
    {
        private readonly TranslateRepository _translateRepository;
        

        public TranslateController(TranslateRepository translateRepository)
        {
            _translateRepository = translateRepository;
            // MALYUTKA BORIS
            
        }

        public async Task<IActionResult> MainMenu()
        {          
            var allTranslates = _translateRepository.GetAll().Take(5).ToList();
          
            
            return View(allTranslates);
        }

        [HttpPost]
        public async Task<IActionResult> SaveChange(List<string> keys, List<string> values)
        {
            return View();
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
                        _translateRepository.Add(new Translate() { EngVariant=translate.key, RusVariant=translate.value});
                    }
                
                }
            }
            catch (Exception ex)
            {
                LoggerTXT.LogServices(ex.Message);
            }
            
            
                return RedirectToAction("MainMenu");
        }
        
       
        
        [HttpGet]
        public async Task<IActionResult> DeleteTranslate(string translateEngName)
        {

            _translateRepository.Delete(translateEngName);
            return RedirectToAction("MainMenu");
            
        }
        
     
    }

}

