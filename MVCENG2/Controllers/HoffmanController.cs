using Microsoft.AspNetCore.Mvc;
using MVCENG2.Data;
using MVCENG2.Interfaces;
using MVCENG2.Models;

namespace MVCENG2.Controllers
{
    public class HoffmanController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStandRepository _standRepository;
        public HoffmanController(ApplicationDbContext context, IStandRepository standRepository)
        {
            _context = context;
            _standRepository = standRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Stand> stands = await _standRepository.GetAll();

            return View(stands);
            
        }
    }
}
