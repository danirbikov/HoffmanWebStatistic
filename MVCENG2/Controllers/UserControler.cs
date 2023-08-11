using Microsoft.AspNetCore.Mvc;
using MVCENG2.Interfaces;
using MVCENG2.Models.General;
using MVCENG2.Repository;
using Microsoft.AspNetCore.Authorization;
using MVCENG2.Services;
using MVCENG2.Models.ViewModel;
using MVCENG2.Models.Hoffman;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MVCENG2.Controllers
{
    [Authorize(Roles = "sa, admin")]
    public class UserController : Controller
    {
        private readonly UsersRepository _usersRepository;

        public UserController(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpGet]
        public async Task<IActionResult> UnactiveUser(int userID)
        {
            _usersRepository.UnactiveUser(userID);          
            return RedirectToAction("MainMenu");

        }

        public async Task<IActionResult> MainMenu()
        {
            ViewData["UserRole"] = HttpContext.User.Claims.Select(k => k.Value).ToList()[1];
            return View(_usersRepository.GetAll().Where(k => k.InactiveMark == "FALSE").ToList());
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(int userID)
        {
            ViewData["UserRole"] = HttpContext.User.Claims.Select(k => k.Value).ToList()[1];
            return View(_usersRepository.GetUserByID(userID));

        }

        [HttpPost]
        public async Task<IActionResult> EditUser(User userObject)
        {
            _usersRepository.EditUser(userObject);
            
            return RedirectToAction("MainMenu");
            
        }

   

    }
}

