using Microsoft.AspNetCore.Mvc;
using SmartShelf.Models.Services;

namespace SmartShelf.Controllers
{
    public class BookController(IBookService bookService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await bookService.GetAllBooksAsync();
            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var book = await bookService.GetBookByIdAsync(id);
            return View(book);
        }
    }
}
