using CoreCrudRelationalDb.Models.Context;
using CoreCrudRelationalDb.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CoreCrudRelationalDb.Controllers
{
    public class CalisanController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CalisanController(ApplicationDbContext db)
        {
            _db = db;
        }
        //[HttpGet]
        public IActionResult Index()
        {
            //Ödev : .NET Core Loading çeşitlerini araştırın!!
            //Ödev 2 : .NET Core data transfer yöntemlerini araştırın!!

            //Include : Her bir çalışanı getirirken, depatman bilgisiyle birlikte getirilecek!
            return View(_db.Calisanlar.Include(x => x.Departman).ToList());
        }

        public IActionResult Create()
        {
            DepartmanlariGetir();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Calisan calisan)
        {
            if (ModelState.IsValid)
            {
                _db.Calisanlar.Add(calisan);
                _db.SaveChanges();
                //return View("Index");
                return RedirectToAction("Index");
            }
            //Validasyonlardan geçemediği takdirde tekrardan sayfaya yolluyorum, fakat departmanları ViewData ile taşıdığım için, tekrardan burada ilgili methodu çağırmam gerekti, aksi takdirde hatalı bir entry sonrası sayfamda departmanları göremem!
            DepartmanlariGetir();
            return View();
        }
        //[HttpGet]
        public IActionResult Delete(int id)
        {
            Calisan calisan = _db.Calisanlar.Find(id);

            if (calisan == null) return NotFound();

            return View(calisan);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmation(int id)
        {
            Calisan calisan = _db.Calisanlar.Find(id);

            if (calisan == null) return NotFound();

            _db.Calisanlar.Remove(calisan);
            _db.SaveChanges();
            TempData["mesaj"] = $"{calisan.TamAd} adlı kişi başarıyla silinmiştir.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Calisan calisan = _db.Calisanlar.Find(id);

            if (calisan == null) return NotFound();

            DepartmanlariGetir();
            return View(calisan);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(Calisan calisan)
        {
            if(ModelState.IsValid)
            {
                //_db.Entry(calisan).State = EntityState.Modified;
                _db.Update(calisan);
                _db.SaveChanges();
                TempData["mesaj"] = $"{calisan.TamAd} adlı kişi başarıyla güncellenmiştir.";
                return RedirectToAction("Index");
            }

            DepartmanlariGetir();
            return View();
        }
      

        private void DepartmanlariGetir()
        {
            //ViewData, ViewBag, TempData  =>  3 tane farklı veri taşıma yöntemi bulunur MVC'de.
            //Bu arkadaşlar dinamiktir, içlerine her türlü yapıyı veya koleksiyonu barındırabilir.
            //En büyük farkı TempData sağlar => ViewData ve ViewBag aslında aynı çalışma mantığına sahiptirler ve içlerine eklenmiş olan veriyi sadace 1 sayfa taşıyabilecek kapasiteleri vardır.
            //Örnek vermek gerekirse, aşağıda oluşturduğumuz departman seçmeye yarayacak olan ViewData, sayfaya kendini gönderdikten sonra, yapılacak herhangi bir post veya get işleminde artık verisini tutamayacaktır!!
            //Eğer 2 sayfa veri taşımak isterseniz ( controllerlar arası haberleşme ) TempData kullanmalısınız!!
            ViewData["Departmanlar"] = _db.Departmanlar.Select(x => new SelectListItem()
            {
                Text = x.Ad,
                Value = x.Id.ToString()
            }).ToList();
        }
    }
}
