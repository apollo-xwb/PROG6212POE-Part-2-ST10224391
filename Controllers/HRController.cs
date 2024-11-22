using Microsoft.AspNetCore.Mvc;

namespace cmcs.Controllers
{
    public class HRController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}