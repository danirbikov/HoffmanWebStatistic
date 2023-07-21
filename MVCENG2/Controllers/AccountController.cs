﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MVCENG2.Models.ViewModel;
using System.Security.Claims;
using MVCENG2.Repository;
using MVCENG2.Models.General;

namespace MVCENG2.Controllers
{
    public class AccountController : Controller
    {
        private UsersRepository _usersRepository;
        public RolesRepository _rolesRepository; 

        public AccountController(UsersRepository context, RolesRepository rolesRepository)
        {
            _usersRepository = context;
            _rolesRepository = rolesRepository;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _usersRepository.GetUserByAuthModel(model);
                if (user != null)
                {
                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Stands");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _usersRepository.GetUserByAuthModel(new LoginModel { Email = model.Email, Password = model.Password });
                if (user == null)
                {
                    
                    // добавляем пользователя в бд
                    User addNewUser = new User
                    {
                        ULogin = model.Email,
                        UPassword = model.Password,
                        Role = _rolesRepository.GetByRoleName("user"),
                        Created = DateTime.Now,
                        InactiveMark = "FALSE"
                    };
                    _usersRepository.Add(addNewUser);

                    await Authenticate(addNewUser); // аутентификация

                    return RedirectToAction("Index", "Stands");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(User user)
        {

            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.ULogin),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.RName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}