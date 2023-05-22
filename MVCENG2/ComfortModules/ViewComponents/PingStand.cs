using Microsoft.AspNetCore.Mvc;

namespace MvcApp.Components
{
    [ViewComponent]
    public class PingStand
    {
        public string Invoke(string ipAddress)
        {
            return "lol";
            
        }
    }
}