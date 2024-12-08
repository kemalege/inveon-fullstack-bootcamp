using InveonBootcamp.Helpers;
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
        public async Task<IActionResult> GetBooks([FromQuery] QueryObject query)
        {
            var books = await bookService.GetAllAsync(query);
            return Ok(books);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = bookService.GetBookById(id);
            return Ok(book);
        }
        
        [HttpPost]
        public IActionResult AddBook([FromBody] BookDto bookDto)
        {
            return CreatedAtAction(nameof(GetBookById), new { id = bookDto.Id }, bookDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var result = bookService.DeleteBook(id);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return StatusCode((int)result.Status, result.Fail);
        }
    }
        
}

