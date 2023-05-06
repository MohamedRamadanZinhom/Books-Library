using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library2.Data;
using Library2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace Library2.Controllers
{
    public class Borrowed_BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<IdentityUser> UserManager { get; set; }

        public Borrowed_BookController(ApplicationDbContext context , UserManager<IdentityUser> user)
        {
            _context = context;
            UserManager = user;
        }

        // GET: Borrowed_Book
        public async Task<IActionResult> Index()
        {

            var x= UserManager.GetUserId(HttpContext.User);
            var data = _context.borrowed_books.Where(b=>b.User_id==x);
            return View(data);
        }

        // GET: Borrowed_Book/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.borrowed_books == null)
            {
                return NotFound();
            }

            var borrowed_Book = await _context.borrowed_books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowed_Book == null)
            {
                return NotFound();
            }

            return View(borrowed_Book);
        }

        // GET: Borrowed_Book/Create

        [Route("borrowed_book/create/{book_id}/{book_title}")]
        public IActionResult Create(int book_id, string book_title)
        {
          
            ViewData["bookid"] = book_id;
            ViewData["booktitle"] = book_title;
            ViewData["userid"]= UserManager.GetUserId(HttpContext.User).ToString();
            
            return View();
        }

       

        // POST: Borrowed_Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("borrowed_book/create/{book_id}/{book_title}")]
        [ValidateAntiForgeryToken]
        public  IActionResult Create(Borrowed_Book borrowed_Book)
        {
            var b =_context.books.Find(borrowed_Book.Book_id );

            if (b.Currentcount > 0)
            {
                if (ModelState.IsValid)
                {
                    b.Currentcount -= 1;
                    
                    _context.Update(b);
                    _context.Add(borrowed_Book);
                    _context.SaveChanges();
                    TempData["Success"] = "Data added Successfully";
                    return RedirectToAction(nameof(Index));
                }
            }
            else 
            {
                TempData["Failed"] = "This operation Not allowed Now ";
                return View();
            }
           
                
                return View(borrowed_Book);


        }

        // GET: Borrowed_Book/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.borrowed_books == null)
            {
                return NotFound();
            }

            var borrowed_Book = await _context.borrowed_books.FindAsync(id);
            if (borrowed_Book == null)
            {
                return NotFound();
            }
            return View(borrowed_Book);
        }

        // POST: Borrowed_Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,client_id,Book_id,Book_Title,Start_Date,End_Date")] Borrowed_Book borrowed_Book)
        {
            if (id != borrowed_Book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrowed_Book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Borrowed_BookExists(borrowed_Book.Id))
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
            return View(borrowed_Book);
        }

        // GET: Borrowed_Book/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.borrowed_books == null)
            {
                return NotFound();
            }

            var borrowed_Book = await _context.borrowed_books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowed_Book == null)
            {
                return NotFound();
            }
         
            return View(borrowed_Book);
        }

        // POST: Borrowed_Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.borrowed_books == null)
            {
                return Problem("Entity set 'ApplicationDbContext.borrowed_books'  is null.");
            }
            var borrowed_Book = await _context.borrowed_books.FindAsync(id);
            if (borrowed_Book != null)
            {
                var b = _context.books.Find(borrowed_Book.Book_id);
              
                if (b.Maxcount != b.Currentcount)
                {
                    _context.borrowed_books.Remove(borrowed_Book);
                    b.Currentcount += 1;
                    _context.Update(b);
                }
                else
                {
                    TempData["Failed"] = "This operation Not allowed Now ";
                   
                }
                
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Borrowed_BookExists(int id)
        {
          return (_context.borrowed_books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
