using proyecto2.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace proyecto2.Controllers
{
    public class AdminController : Controller
    {
        public ApplicationDbContext db;
        public AdminController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult ReviewList()
        {
            var reviews = db.Reviews.ToList();
            return View(reviews);
        }

        [HttpGet]
        public IActionResult DeleteReview(int id)
        {
            var entry = db.Reviews.Find(id);
            db.Reviews.Remove(entry);
            db.SaveChanges();
            return RedirectToAction("ReviewList");
        }
    }
}
