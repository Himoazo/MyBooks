using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBooks.Data;
using MyBooks.DTOs;
using MyBooks.Helpers;
using MyBooks.Models;

namespace MyBooks.Controllers
{
    public class BorrowingsController : Controller
    {
        private readonly MyBooksDbContext _context;

        public BorrowingsController(MyBooksDbContext context)
        {
            _context = context;
        }

        // GET: Borrowings
        public async Task<IActionResult> Index()
        {
            var myBooksDbContext = _context.Borrowings.Include(b => b.Book).Include(b => b.User);
            return View(await myBooksDbContext.ToListAsync());
        }

        // GET: Borrowings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowings
                .Include(b => b.Book)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BorrowingID == id);
            if (borrowing == null)
            {
                return NotFound();
            }

            return View(borrowing);
        }

        // GET: Borrowings/Create
        public async Task<IActionResult> Create(int? id)
        {
            var books = await _context.Books.ToListAsync();
            var users = await _context.Users.ToListAsync();

            if (id != null)
            {
                ViewData["BookID"] = new SelectList(books, "BookID", "Book_name", id);
            } 
            else 
            {
                ViewData["BookID"] = new SelectList(books, "BookID", "Book_name");
            }

            User? LoggedInUser = SessionHelper.LoggedInUser(HttpContext);

           
            ViewData["UserID"] = LoggedInUser != null ? LoggedInUser.UserID : 0;
            ViewData["Borrow_date"] = DateOnly.FromDateTime(DateTime.Today).ToString("yyyy-MM-dd");
            return View();
        }

        // POST: Borrowings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Borrow_date,UserID,BookID")] BorrowingDTO borrowingDto)
        {
            Borrowing borrowing = new Borrowing
            {
                Borrow_date = borrowingDto.Borrow_date,
                UserID = borrowingDto.UserID,
                BookID = borrowingDto.BookID,
                Due_date = borrowingDto.Borrow_date.AddDays(10)
            };

            if(borrowingDto.UserID == 0)
            {
                ModelState.AddModelError(string.Empty, "Du måste vara inloggad för att kunna låna böcker");
                return View(borrowing);
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Bokifno eller datum är felaktiga");
                return View(borrowing);
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.BookID == borrowingDto.BookID);

            

            if (book == null || book.Available == false)
            {
                ModelState.AddModelError(string.Empty, "Boken är utlånad");
                return View(borrowing);
            }

            if (borrowingDto.Borrow_date < DateOnly.FromDateTime(DateTime.Today))
            {
                ModelState.AddModelError("Borrow_date", "Lånedatum kan inte vara tidigare än idag");
                return View(borrowing);
            }

            if (ModelState.IsValid) 
            {
                book.Available = false;
                _context.Add(borrowing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["BookID"] = new SelectList(_context.Books, "BookID", "Book_name", borrowing.BookID);
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "User_Name", borrowing.UserID);
            return View(borrowing);
        }

        // Get borrowed book
        // GET: Borrowings/Edit/5
        public async Task<IActionResult> Borrow(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var borrowing = await _context.Borrowings.FindAsync(id);
            if (borrowing == null)
            {
                return NotFound();
            }
            ViewData["Return_date"] = DateOnly.FromDateTime(DateTime.Today).ToString("yyyy-MM-dd");
            ViewData["BookID"] = new SelectList(_context.Books, "BookID", "Book_name", borrowing.BookID);
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "User_Name", borrowing.UserID);
            return View(borrowing);
        }

        // Return borrowed book
        // POST: Borrowings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Borrow(int id, [Bind("BorrowingID,Return_date,BookID")] ReturnBookDTO borrowing)
        {
           
            if (id != borrowing.BorrowingID)
            {
                return NotFound();
            }
            var retunedBook = await _context.Borrowings.FindAsync(borrowing.BorrowingID);

            if (retunedBook?.Return_date != null) 
            {
                ModelState.AddModelError(string.Empty, "Boken har redan återlämnats");
                return View(retunedBook);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var book = await _context.Books.FindAsync(borrowing.BookID);

                    if (retunedBook is not null) 
                    {
                        retunedBook.Return_date = DateOnly.FromDateTime(DateTime.Today);
                        _context.Update(retunedBook);
                    }

                    if (book is not null) 
                    {
                        book.Available = true;
                    }
                    
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowingExists(borrowing.BorrowingID))
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
            ViewData["BookID"] = new SelectList(_context.Books, "BookID", "BookID", borrowing.BookID);
            return View(retunedBook);
        }

        // GET: Borrowings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowings
                .Include(b => b.Book)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BorrowingID == id);
            if (borrowing == null)
            {
                return NotFound();
            }

            return View(borrowing);
        }

        // POST: Borrowings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var borrowing = await _context.Borrowings.FindAsync(id);

            if (borrowing != null && borrowing.Return_date == null)
            {
                ModelState.AddModelError(string.Empty, "Boken måste återlämnas först innan lånedata kan raderas");
                return View(borrowing);
            }

            if (borrowing != null)
            {
                _context.Borrowings.Remove(borrowing);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowingExists(int id)
        {
            return _context.Borrowings.Any(e => e.BorrowingID == id);
        }
    }
}
