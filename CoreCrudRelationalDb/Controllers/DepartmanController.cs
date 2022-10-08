using CoreCrudRelationalDb.Models.Context;
using CoreCrudRelationalDb.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoreCrudRelationalDb.Controllers
{
    public class DepartmanController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DepartmanController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Departmanlar.ToList());
        }

        // HttpGet => url'e gelen istek. url ise => localhost3405/Departman/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Departman departman)
        {
            if(ModelState.IsValid)
            {
                _db.Departmanlar.Add(departman);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        // HttpGet => url'e gelen istek. url ise => localhost3405/Departman/Delete/5

        public IActionResult Delete(int id)
        {
            Departman departman = _db.Departmanlar.Find(id);

            if (departman == null) return NotFound();

            _db.Departmanlar.Remove(departman);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Departman departman = _db.Departmanlar.Find(id);

            if (departman == null) return NotFound();

            return View(departman);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Departman departman)
        {
            if(ModelState.IsValid)
            {
                _db.Update(departman);
                _db.SaveChanges();
                TempData["mesaj"] = $"{departman.Ad} adlı departman güncellemesi gerçekleştirildi.";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
