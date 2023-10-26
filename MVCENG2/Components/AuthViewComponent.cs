


using HoffmanWebstatistic.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HoffmanWebstatistic.Components
{
 
    public class AuthViewComponent
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthViewComponent(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> InvokeAsync(string method)
        {
            string roleNameResult = "";
            switch (method)
            {
                case "Role":
                    roleNameResult = GetUserRole();
                    break;
                case "Name":
                    roleNameResult = GetUserName();
                    break;
            }

            if (roleNameResult == null)
                roleNameResult = "not authorized";

            return roleNameResult;
        }

        private string GetUserRole()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
        }

        private string GetUserName()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name); 
        }
    }    
}
