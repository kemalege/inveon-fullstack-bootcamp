using System.Net;
using InveonBootcamp.Models.Repositories;
using InveonBootcamp.Shared;

namespace InveonBootcamp.Models
{
    public class BookService(BookRepository bookRepository)
    {
        public async Task<List<BookDto>> GetBooks()
        {
         
            var books= await bookRepository.GetAllBooks();
            var booksAsDto  = books.Select(b => new BookDto()
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Description = b.Description,
                Price = b.Price
            }).ToList();
       
            return booksAsDto;
        }

        public BookDto GetBookById(int id)
        {
            var book = bookRepository.GetBookById(id);

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                Price = book.Price
            };
        }

    }
}