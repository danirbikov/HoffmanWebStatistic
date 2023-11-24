using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using HoffmanWebstatistic.Models.ViewModel;
using System.Security.Claims;
using HoffmanWebstatistic.Repository;
using HoffmanWebstatistic.Models.Hoffman;
using Microsoft.AspNetCore.Authorization;

namespace HoffmanWebstatistic.Controllers
{
    public class AccountController : Controller
    {
        private UsersRepository _usersRepository;
        public RolesRepository _rolesRepository; 

        public AccountController(UsersRepository usersRepository, RolesRepository rolesRepository)
        {
            _usersRepository = usersRepository;
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

                    return RedirectToAction("Index", "Home");
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
                if (!_usersRepository.AnyUserByName(model.Email))
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

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("Email", "Пользователь с таким логином уже существует");
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
        public IActionResult AccessDenied()
        {
            return View("CustomErrorPage", "Недостаточно прав доступа для просмотра страницы. Обратитесь к администраторам сервиса:\n\nMalyutkinBE@kamaz.ru\nUmyarovAR@kamaz.ru\nBikovDI@kamaz.ru");
        }
    }
}