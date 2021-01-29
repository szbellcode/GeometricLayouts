using Logic.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public AppConfig AppConfig { get; }

        public HomeController(AppConfig appConfig)
        {
            AppConfig = appConfig;
        }
        public IActionResult Index()
        {
            return View(AppConfig);
        }
    }
}
