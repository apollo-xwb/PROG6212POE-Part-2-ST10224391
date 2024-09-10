using Microsoft.AspNetCore.Mvc;
using CMCS.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        public IActionResult Index()
        {
            var claims = _context.Claims.Include(c => c.SupportingDocuments).ToList();
            return View(claims);
        }

        // GET: Claims/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Claims/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Claim claim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(claim);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(claim);
        }

        // GET: Claims/Edit/5
        public IActionResult Edit(int id)
        {
            var claim = _context.Claims.Find(id);
            if (claim == null)
            {
                return NotFound();
            }
            return View(claim);
        }

        // POST: Claims/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Claim claim)
        {
            if (id != claim.ClaimID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(claim);
                    _context.SaveChanges();
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

        // GET: Claims/Delete/5
        public IActionResult Delete(int id)
        {
            var claim = _context.Claims.Find(id);
            if (claim == null)
            {
                return NotFound();
            }
            return View(claim);
        }

        // POST: Claims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var claim = _context.Claims.Find(id);
            _context.Claims.Remove(claim);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Claims/Details/5
        public IActionResult Details(int id)
        {
            var claim = _context.Claims
                .Include(c => c.SupportingDocuments)
                .FirstOrDefault(c => c.ClaimID == id);

            if (claim == null)
            {
                return NotFound();
            }
            return View(claim);
        }

        // POST: Claims/CreateClaim
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateClaim(Claim claim, List<IFormFile> supportingDocuments)
        {
            if (ModelState.IsValid)
            {
                // Adds any claims to the database
                _context.Claims.Add(claim);
                await _context.SaveChangesAsync();

                // Handles all the file uploads
                foreach (var file in supportingDocuments)
                {
                    if (file.Length > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/documents", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        var document = new SupportingDocument
                        {
                            DocumentName = fileName,
                            DocumentPath = $"/documents/{fileName}",
                            ClaimID = claim.ClaimID
                        };

                        _context.SupportingDocuments.Add(document);
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(claim);
        }

        // GET: Claims/DownloadDocument/5
        public IActionResult DownloadDocument(int id)
        {
            var document = _context.SupportingDocuments.Find(id);
            if (document == null)
            {
                return NotFound();
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", document.DocumentPath);
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/octet-stream", document.DocumentName);
        }

        private bool ClaimExists(int id)
        {
            return _context.Claims.Any(e => e.ClaimID == id);
        }
    }
}
