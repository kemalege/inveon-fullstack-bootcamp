using InveonBootcamp.Models;
using InveonBootcamp.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace InveonBootcamp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(BookService bookService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await bookService.GetBooks();
            return Ok(books);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = bookService.GetBookById(id);
            return Ok(book);
        }
        
        [HttpPost(Name = "add")]
        public IActionResult AddBook([FromBody] BookDto bookDto)
        {
            return CreatedAtAction(nameof(GetBookById), new { id = bookDto.Id }, bookDto);
        }

        [HttpPut("{id}/update")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            return NoContent();
        }

        [HttpDelete("{id}/delete")]
        public IActionResult DeleteBook(int id)
        {
            return NoContent();
        }
    }
        
}

