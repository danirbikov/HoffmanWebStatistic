using Microsoft.AspNetCore.Mvc;
using HoffmanWebstatistic.Repository;
using Microsoft.AspNetCore.Authorization;
using HoffmanWebstatistic.Models.ViewModel;
using HoffmanWebstatistic.Services.Job;
using HoffmanWebstatistic.Models.Hoffman;
using HoffmanWebstatistic.Services.InteractionStand;

namespace HoffmanWebstatistic.Controllers
{
    [Authorize(Roles = "sa")]
    public class MESController : Controller
    {

        private readonly Mes2SupTelegramsRepository _mes2SupTelegramsRepository;
        private readonly Sup2MesTelegramsRepository _sup2MesTelegramsRepository;
        private readonly XSDSchemasRepository _xsdSchemasRepository;
        private readonly XSDSchemasPurposeRepository _xsdSchemasPurposeRepository;

        public MESController(Mes2SupTelegramsRepository mes2SupTelegramsRepository, Sup2MesTelegramsRepository sup2MesTelegramsRepository, XSDSchemasRepository xsdSchemasRepository, XSDSchemasPurposeRepository xsdSchemasPurposeRepository)
        {
            _mes2SupTelegramsRepository = mes2SupTelegramsRepository;
            _sup2MesTelegramsRepository = sup2MesTelegramsRepository;
            _xsdSchemasRepository = xsdSchemasRepository;
            _xsdSchemasPurposeRepository = xsdSchemasPurposeRepository;
        }

        [Authorize(Roles = "sa")]
        public async Task<IActionResult> MainMenu()
        {
            return View(new MESViewModel()
            {
                sup2MesCount = _sup2MesTelegramsRepository.GetTelegramsCount(),
                mes2SupCount = _mes2SupTelegramsRepository.GetTelegramsCount(),
                xsdSchemas = _xsdSchemasRepository.GetAllWithInclude()
            });
        }

        [HttpGet]
        public async Task<IActionResult> AddXsdSchema()
        {
            ViewData["XSDPurposes"] = _xsdSchemasPurposeRepository.GetAll();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddXsdSchema(IFormFile xsdSchemaFile, int PurposeId, string XsdDescription)
        {

            using (var memoryStream = new MemoryStream())
            {
                await xsdSchemaFile.CopyToAsync(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();

                XsdSchema xsdSchema = new XsdSchema() { XsdSchemaFile = fileBytes, XsdDescription = XsdDescription, PurposeId = PurposeId, Created=DateTime.Now };

                _xsdSchemasRepository.Add(xsdSchema);
                
            }

            return RedirectToAction("MainMenu");
        }

        [HttpGet]
        public async Task<IActionResult> EditXSDSchema(int xsdSchemaId)
        {
            ViewData["XSDPurposes"] = _xsdSchemasPurposeRepository.GetAll();
            return View(_xsdSchemasRepository.GetXSDSchemaByID(xsdSchemaId));

        }

        [HttpPost]
        public async Task<IActionResult> EditXsdSchema(IFormFile xsdSchemaFile, int PurposeId, string XsdDescription, int xsdSchemaId)
        {

            if (xsdSchemaFile == null)
            {
                XsdSchema xsdSchema = new XsdSchema() { Id = xsdSchemaId, XsdSchemaFile = null, XsdDescription = XsdDescription, PurposeId = PurposeId, Created = DateTime.Now };
                _xsdSchemasRepository.EditXSDSchema(xsdSchema);
            }
            else
            {
                using (var memoryStream = new MemoryStream())
                {
                    await xsdSchemaFile.CopyToAsync(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();

                    XsdSchema xsdSchema = new XsdSchema() { Id = xsdSchemaId, XsdSchemaFile = fileBytes, XsdDescription = XsdDescription, PurposeId = PurposeId, Created = DateTime.Now };

                    _xsdSchemasRepository.EditXSDSchema(xsdSchema);

                }
            }
                      

            return RedirectToAction("MainMenu");

        }
        [HttpGet]
        public async Task<IActionResult> DeleteXSDSchema(int xsdSchemaId)
        {
            _xsdSchemasRepository.DeleteXSDSchema(xsdSchemaId);

            return RedirectToAction("MainMenu");

        }
    }
}

