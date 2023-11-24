using Microsoft.AspNetCore.Mvc;
using HoffmanWebstatistic.Repository;
using Microsoft.AspNetCore.Authorization;
using HoffmanWebstatistic.Models.ViewModel;
using HoffmanWebstatistic.Services.Job;
using HoffmanWebstatistic.Models.Hoffman;
using HoffmanWebstatistic.Services.InteractionStand;
using static HoffmanWebstatistic.Models.Enums.SortingEnum;
using System.Globalization;
using System.Xml.Linq;
using HoffmanWebstatistic.Models.SerializerModels;
using System.Xml.Serialization;
using System.Xml;

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

        public async Task<IActionResult> MesTelegrams(string mesSchemaTypeIdentifier = "sup2mes", int pageNumber = 1, SortState sortOrder = SortState.DateDesc, string startDate = "", string endDate = "", int pageSize = 10)
        {
            if (mesSchemaTypeIdentifier== "sup2mes")
            {
                return View("Sup2Mes", Sup2MesTelegrams(mesSchemaTypeIdentifier, pageNumber, sortOrder, startDate, endDate, pageSize));
            }
            else
            {
                return View("Mes2Sup", Mes2SupTelegrams(mesSchemaTypeIdentifier, pageNumber, sortOrder, startDate, endDate, pageSize));
            }
            
        }


        public IEnumerable<Mes2supTelegram> Mes2SupInitializePageValue(int pageNumber, SortState sortOrder)
        {

            var resultsMes2SupTelegrmas = _mes2SupTelegramsRepository.GetAllQuery();

            // сортировка
            switch (sortOrder)
            {
                case SortState.VINDesc:
                    resultsMes2SupTelegrmas = resultsMes2SupTelegrmas.OrderByDescending(s => s.Vin);
                    break;
                case SortState.OrderNumberAsc:
                    resultsMes2SupTelegrmas = resultsMes2SupTelegrmas.OrderBy(s => s.Ordernum);
                    break;
                case SortState.OrderNumberDesc:
                    resultsMes2SupTelegrmas = resultsMes2SupTelegrmas.OrderByDescending(s => s.Ordernum);
                    break;
                case SortState.StandNameAsc:
                    resultsMes2SupTelegrmas = resultsMes2SupTelegrmas.OrderBy(s => s.Mes2supTelegramsStands.FirstOrDefault().Stand.StandName);
                    break;
                case SortState.StandNameDesc:
                    resultsMes2SupTelegrmas = resultsMes2SupTelegrmas.OrderByDescending(s => s.Mes2supTelegramsStands.FirstOrDefault().Stand.StandName);
                    break;
                case SortState.DateAsc:
                    resultsMes2SupTelegrmas = resultsMes2SupTelegrmas.OrderBy(s => s.Created);
                    break;
                case SortState.DateDesc:
                    resultsMes2SupTelegrmas = resultsMes2SupTelegrmas.OrderByDescending(s => s.Created);
                    break;

                default:
                    resultsMes2SupTelegrmas = resultsMes2SupTelegrmas.OrderBy(s => s.Vin);
                    break;
            }

            return resultsMes2SupTelegrmas;
        }

        public Mes2SupViewModel Mes2SupTelegrams(string mesSchemaTypeIdentifier = "sup2mes", int pageNumber = 1, SortState sortOrder = SortState.DateDesc, string startDate = "", string endDate = "", int pageSize = 10)
        {
            DateTime parsedEndDate = new DateTime();
            DateTime parsedStartDate = new DateTime();

            CultureInfo russianCulture = new CultureInfo("ru-RU");
            if (endDate == "" || startDate == "")
            {
                parsedEndDate = DateTime.Now;
            }
            else
            {
                parsedStartDate = DateTime.Parse(startDate, russianCulture);
                parsedEndDate = DateTime.Parse(endDate, russianCulture);
            }

            ViewData["MesSchemaTypeIdentifier"] = mesSchemaTypeIdentifier;
            ViewData["PageSize"] = pageSize;
            ViewData["StartDate"] = parsedStartDate;
            ViewData["EndDate"] = parsedEndDate;

            // пагинация

            var jsonHeaderIDs = Mes2SupInitializePageValue(pageNumber, sortOrder).Where(k => k.Created >= parsedStartDate && k.Created <= parsedEndDate.AddDays(1)).Select(k => k.Id).ToList();
            var count = jsonHeaderIDs.Count();
            var itemsId = jsonHeaderIDs.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();


            Mes2SupViewModel viewModel = new Mes2SupViewModel(
                _mes2SupTelegramsRepository.GetMes2SupTelegramById(itemsId),
                new PageViewModel(count, pageNumber, pageSize),
                new SortViewModel(sortOrder)
            );

            return viewModel;
        }


        public IEnumerable<Sup2mesTelegram> Sup2MesInitializePageValue( int pageNumber, SortState sortOrder)
        {

             var resultsMes2SupTelegrmas = _sup2MesTelegramsRepository.GetAllQuery();

            // сортировка
            switch (sortOrder)
            {
                case SortState.VINDesc:
                    resultsMes2SupTelegrmas = resultsMes2SupTelegrmas.OrderByDescending(s => s.Vin);
                    break;
                case SortState.OrderNumberAsc:
                    resultsMes2SupTelegrmas = resultsMes2SupTelegrmas.OrderBy(s => s.Ordernum);
                    break;
                case SortState.OrderNumberDesc:
                    resultsMes2SupTelegrmas = resultsMes2SupTelegrmas.OrderByDescending(s => s.Ordernum);
                    break;
                case SortState.StandNameAsc:
                    resultsMes2SupTelegrmas = resultsMes2SupTelegrmas.OrderBy(s => s.Stand.StandName);
                    break;
                case SortState.StandNameDesc:
                    resultsMes2SupTelegrmas = resultsMes2SupTelegrmas.OrderByDescending(s => s.Stand.StandName);
                    break;
                case SortState.DateAsc:
                    resultsMes2SupTelegrmas = resultsMes2SupTelegrmas.OrderBy(s => s.Created);
                    break;
                case SortState.DateDesc:
                    resultsMes2SupTelegrmas = resultsMes2SupTelegrmas.OrderByDescending(s => s.Created);
                    break;

                default:
                    resultsMes2SupTelegrmas = resultsMes2SupTelegrmas.OrderBy(s => s.Vin);
                    break;
            }

            return resultsMes2SupTelegrmas;
        }

        public Sup2MesViewModel Sup2MesTelegrams(string mesSchemaTypeIdentifier = "sup2mes",  int pageNumber = 1, SortState sortOrder = SortState.DateDesc, string startDate = "", string endDate = "", int pageSize = 10)
        {
            DateTime parsedEndDate = new DateTime();
            DateTime parsedStartDate = new DateTime();

            CultureInfo russianCulture = new CultureInfo("ru-RU");
            if (endDate == "" || startDate == "")
            {
                parsedEndDate = DateTime.Now;
            }
            else
            {
                parsedStartDate = DateTime.Parse(startDate, russianCulture);
                parsedEndDate = DateTime.Parse(endDate, russianCulture);
            }

            ViewData["MesSchemaTypeIdentifier"] = mesSchemaTypeIdentifier;
            ViewData["PageSize"] = pageSize;
            ViewData["StartDate"] = parsedStartDate;
            ViewData["EndDate"] = parsedEndDate;

            // пагинация

            var jsonHeaderIDs = Sup2MesInitializePageValue(pageNumber, sortOrder).Where(k => k.Created >= parsedStartDate && k.Created <= parsedEndDate.AddDays(1)).Select(k => k.Id).ToList();
            var count = jsonHeaderIDs.Count();
            var itemsId = jsonHeaderIDs.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();


            Sup2MesViewModel viewModel = new Sup2MesViewModel(
                _sup2MesTelegramsRepository.GetSup2mesTelegramById(itemsId),
                new PageViewModel(count, pageNumber, pageSize),
                new SortViewModel(sortOrder)
            );

            return viewModel;
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
        public async Task<IActionResult> Mes2SupTelegram(int mes2SupTelegramId)
        {
            var mes2SupTelegramObject = _mes2SupTelegramsRepository.GetMes2SupTelegramById(mes2SupTelegramId);

            using (TextReader xmlTextReadert = new StringReader(mes2SupTelegramObject.TgContent))
            {
                using (XmlReader reader = XmlReader.Create(xmlTextReadert))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(MES_VEHICLE_STATUS_DESCRIPTOR));
                    MES_VEHICLE_STATUS_DESCRIPTOR deserializeMesObject = (MES_VEHICLE_STATUS_DESCRIPTOR)xmlSerializer.Deserialize(reader);
                    
                    return View(deserializeMesObject);
                }
            }           
        }

        [HttpGet]
        public async Task<IActionResult> Sup2MesTelegram(int sup2MesTelegramId)
        {
            var sup2MesTelegramObject = _sup2MesTelegramsRepository.GetSup2MesTelegramById(sup2MesTelegramId);

            using (TextReader xmlTextReadert = new StringReader(sup2MesTelegramObject.TgContent))
            {
                using (XmlReader reader = XmlReader.Create(xmlTextReadert))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(SHOP_FLOOR_DATA));
                    SHOP_FLOOR_DATA deserializeMesObject = (SHOP_FLOOR_DATA)xmlSerializer.Deserialize(reader);

                    return View(deserializeMesObject);
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteXSDSchema(int xsdSchemaId)
        {
            _xsdSchemasRepository.DeleteXSDSchema(xsdSchemaId);

            return RedirectToAction("MainMenu");

        }
    }
}

