using Microsoft.AspNetCore.Mvc;
using SmartShelf.Models.Repositories;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SmartShelf.Controllers
{
    public class BookController(AppDbContext context) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var books = context.Books.ToList();
            return View(books);
        }
        
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var book = await context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return NotFound("Book not found.");
            }
            return View(book);
        }

    }
    
}