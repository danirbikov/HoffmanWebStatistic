using Microsoft.AspNetCore.Mvc;
using HoffmanWebstatistic.Repository;
using Microsoft.AspNetCore.Authorization;
using HoffmanWebstatistic.Services;
using HoffmanWebstatistic.Models.ViewModel;
using HoffmanWebstatistic.Models.Hoffman;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace HoffmanWebstatistic.Controllers
{
    [Authorize(Roles = "sa")]
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
        [HttpGet]
        public async Task<IActionResult> InactiveUser(int userID)
        {
            _usersRepository.InactiveUser(userID);
            return RedirectToAction("MainMenu");
        }

        public async Task<IActionResult> MainMenu()
        {
            ViewData["UserRole"] = HttpContext.User.Claims.Select(k => k.Value).ToList()[1];

            List<User> users = new List<User>();
            if (User.IsInRole("sa"))
            {
                users = _usersRepository.GetAllWitInactive().ToList();
            }
            else
            {
                users = _usersRepository.GetAll().ToList();
            }

            return View(users);
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

