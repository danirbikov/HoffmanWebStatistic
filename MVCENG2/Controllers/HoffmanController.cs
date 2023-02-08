using Microsoft.AspNetCore.Mvc;
using MVCENG2.Data;
using MVCENG2.Models;

namespace MVCENG2.Controllers
{
    public class HoffmanController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HoffmanController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
