using Microsoft.AspNetCore.Mvc;

namespace AppManagerServer.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Application Manager Server \nGo to /application for available requests";
        }
    }
}
