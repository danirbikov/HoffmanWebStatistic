using Microsoft.AspNetCore.Mvc;
using MVCENG2.Interfaces;
using MVCENG2.Models.General;

namespace MVCENG2.Controllers
{
    public class AdminController : Controller
    {
        private readonly IStandRepository _standRepository;
        public AdminController(IStandRepository standRepository)
        {
            _standRepository = standRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddStand(Stand stand)
        {
            if (!ModelState.IsValid)
            {
                return View(stand);
            }
            _standRepository.Add(stand);
            return RedirectPermanent("/Home/Index");
        }
    }
}
