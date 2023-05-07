using Library2.Data;
using Microsoft.AspNetCore.Mvc;
using Library2.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Library2.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext _db;
        private UserManager<IdentityUser> UserManager { get; set; }
        public BooksController(ApplicationDbContext db, UserManager<IdentityUser> user)
        {
            _db = db;
            UserManager = user;
        }

        public IActionResult Index()
        {
            var list = _db.books.ToList();
            ViewBag.userid= UserManager.GetUserId(HttpContext.User);
            return View(list);
        }


        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Book b)
        {
            if (ModelState.IsValid)
            {
                _db.books.Add(b);
                _db.SaveChanges();
                TempData["Success"] = "Data added Successfully";
                return View();
            }

            return View(b);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Book InsertBook(Book book)
        {
            if (ModelState.IsValid)
            {

                _db.books.Add(book);
                _db.SaveChanges();
                TempData["Success"] = "Data Updated Successfully";
            }
            return book;
        }


        [Route("book/edit/{id}")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }


            var data = _db.books.Find(id);
            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("book/edit/{id}")]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                _db.books.Update(book);

                _db.SaveChanges();
                TempData["Success"] = "Data Updated Successfully";
                return RedirectToAction("Index", "Book");
            }
            return View(book);

        }



   




    }
}
