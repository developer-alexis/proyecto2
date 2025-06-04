using proyecto2.Data;
using proyecto2.Helpers;
using proyecto2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace proyecto2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registro(Account obj)
        {
            db.Accounts.Add(obj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Foro()
        {
            var posts = db.Posts.Include(p => p.Account).ToList();
            return View(posts);
        }

        public IActionResult Productos()
        {
            return View();
        }

        public IActionResult Contacto()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contacto(Review obj)
        {
            db.Reviews.Add(obj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult InicioSesion()
        {
            return View();
        }
        [HttpPost]
        public IActionResult InicioSesion(string email, string password)
        {
            var account = db.Accounts.FirstOrDefault(a => a.Email == email && a.Password == password);
            if (account != null && account.AccountStatus == AccountStatus.Active)
            {
                HttpContext.Session.SetString("UserId", account.Id.ToString());
                HttpContext.Session.SetString("UserName", account.Name);
                HttpContext.Session.SetString("UserRole", account.AccountRole.ToString());
                HttpContext.Session.SetString("UserEmail", account.Email);
                return RedirectToAction("Index");
            }

            ViewBag.Error = "Email o contraseña incorrecta";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(Post obj, IFormFile PostPicPath)
        {
            string userIdString = SessionHelper.GetUserId(HttpContext);
            obj.AccountId = Convert.ToInt32(userIdString);

            if (PostPicPath != null && PostPicPath.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(PostPicPath.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("PostPicPath", "Solo se permiten archivos JPG, JPEG, PNG o GIF.");
                    return View(obj);
                }

                var fileName = Guid.NewGuid().ToString() + extension;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads"));

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    PostPicPath.CopyTo(stream);
                }

                obj.PostPicPath = "/uploads/" + fileName;
            }

            db.Posts.Add(obj);
            db.SaveChanges();
            return RedirectToAction("Foro");
        }

        [HttpGet]
        public IActionResult DeletePost(int id)
        {
            var entry = db.Posts.Find(id);
            db.Posts.Remove(entry);
            db.SaveChanges();
            return RedirectToAction("Foro");
        }

        [HttpGet]
        public IActionResult EditPost(int id)
        {
            var entry = db.Posts.Find(id);
            if (entry != null && entry.AccountId == Convert.ToInt32(SessionHelper.GetUserId(HttpContext)))
            {
                return View(entry);
            }
            else return NotFound();
        }

        [HttpPost]
        public IActionResult EditPost(Post model, IFormFile PostPicPath)
        {
            var post = db.Posts.FirstOrDefault(p => p.Id == model.Id);
            if (post == null)
            {
                return NotFound();
            }

            post.Title = model.Title;
            post.Description = model.Description;
            post.PostType = model.PostType;


            if (PostPicPath != null && PostPicPath.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(PostPicPath.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("PostPicPath", "Solo se permiten archivos JPG, JPEG, PNG o GIF.");
                    return View(model);
                }

                var fileName = Guid.NewGuid().ToString() + extension;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads"));
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    PostPicPath.CopyTo(stream);
                }
                post.PostPicPath = "/uploads/" + fileName;
            }

            db.SaveChanges();
            return RedirectToAction("Foro");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}