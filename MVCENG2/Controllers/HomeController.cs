using Microsoft.AspNetCore.Mvc;
using MVCENG2.Interfaces;
using MVCENG2.Models.General;
using MVCENG2.Repository;
using Microsoft.AspNetCore.Authorization;
using MVCENG2.Services;
using MVCENG2.Models.ViewModel;
using Microsoft.VisualBasic;

namespace MVCENG2.Controllers
{
   
    public class HomeController : Controller
    {

        //[Authorize(Roles = "sa")]
        public async Task<IActionResult> Index()
        {           
            


            return View();

        }
    }
}

