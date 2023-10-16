using Microsoft.AspNetCore.Mvc;
using HoffmanWebstatistic.Interfaces;
using HoffmanWebstatistic.Repository;
using Microsoft.AspNetCore.Authorization;
using HoffmanWebstatistic.Services;
using HoffmanWebstatistic.Models.ViewModel;
using HoffmanWebstatistic.Models.Hoffman;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace HoffmanWebstatistic.Controllers
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

