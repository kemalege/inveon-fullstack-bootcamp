using System.Net;
using InveonBootcamp.Helpers;
using InveonBootcamp.Models.Repositories;
using InveonBootcamp.Shared;

namespace InveonBootcamp.Models
{
    public class BookService(BookRepository bookRepository)
    {
        public async Task<List<BookDto>> GetAllAsync(QueryObject query)
        {
            var skipNumber =(query.PageNumber -1) * query.PageSize;
            
            var books = await bookRepository.GetAllBooksAsync(skipNumber, query.PageSize);
         
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
        
        public ServiceResult DeleteBook(int id)
        {
            var book = bookRepository.GetBookById(id);
            if (book == null)
            {
                return ServiceResult<Guid>.Error("Book not found.", $"The Book with ID {id} was not found", HttpStatusCode.NotFound);
            }

            bookRepository.DeleteBook(id);
            return ServiceResult.SuccessAsNoContent();
        }

    }
}