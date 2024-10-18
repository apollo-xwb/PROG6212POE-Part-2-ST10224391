using Microsoft.AspNetCore.Mvc;
using CMCS.Models;

namespace CMCS.Controllers
{
    public class LecturersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LecturersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lecturers = _context.Lecturers.ToList();
            return View(lecturers);
        }
    }
}
