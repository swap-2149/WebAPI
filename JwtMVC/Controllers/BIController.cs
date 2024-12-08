using Microsoft.AspNetCore.Mvc;

namespace JwtMVC.Controllers
{
    public class BIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        
    }
}
