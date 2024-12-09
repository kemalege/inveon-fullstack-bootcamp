using InveonBootcamp.Helpers;
using InveonBootcamp.Models;
using InveonBootcamp.Models.Caching;
using InveonBootcamp.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace InveonBootcamp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(BookService bookService, IRedisCacheService cache) : ControllerBase
    
    {
        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] QueryObject query)
        {
            var userId = Request.Headers["UserId"];
            
            var cachingKey = $"books_{userId}";
            var books = cache.GetData<IEnumerable<BookDto>>(cachingKey);
            if (books is not null)
            {
                return Ok(books);
            }
            books = await bookService.GetAllAsync(query);
            cache.SetData(cachingKey, books);
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

