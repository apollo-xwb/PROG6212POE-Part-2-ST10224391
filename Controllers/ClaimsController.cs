using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMCS.Models;
using System.Threading.Tasks;
using System.IO;

namespace CMCS.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClaimsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Claims
        public async Task<IActionResult> Index()
        {
            var claims = _context.Claims.Include(c => c.Lecturer);
            return View(await claims.ToListAsync());
        }

        // GET: Claims/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Claims/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClaimID,LecturerID,Amount,ClaimDate,Status,Description,HoursWorked,HourlyRate,AdditionalNotes,Document")] Claim claim)
        {
            if (ModelState.IsValid)
            {
                if (claim.Document != null)
                {
                    // Handle file upload
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/documents", claim.Document.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await claim.Document.CopyToAsync(stream);
                    }
                }

                _context.Add(claim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(claim);
        }

        // GET: Claims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claim = await _context.Claims.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }
            return View(claim);
        }

        // POST: Claims/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClaimID,LecturerID,Amount,ClaimDate,Status,Description,HoursWorked,HourlyRate,AdditionalNotes,Document")] Claim claim)
        {
            if (id != claim.ClaimID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (claim.Document != null)
                    {
                        // Handle file upload
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/documents", claim.Document.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await claim.Document.CopyToAsync(stream);
                        }
                    }

                    _context.Update(claim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaimExists(claim.ClaimID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(claim);
        }

        private bool ClaimExists(int id)
        {
            return _context.Claims.Any(e => e.ClaimID == id);
        }
    }
}
