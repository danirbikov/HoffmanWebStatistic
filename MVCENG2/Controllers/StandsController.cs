using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MVCENG2.Data;
using MVCENG2.Interfaces;
using PagedList;
using MVCENG2.Services;
using MVCENG2.Models.General;

namespace MVCENG2.Controllers
{
    public class StandsController : Controller
    {

        private readonly IStandRepository _standRepository;

        public StandsController(IStandRepository standRepository)
        {
            _standRepository = standRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Stand> stands = _standRepository.GetAll();


            return View(stands);




        }
    }
}
