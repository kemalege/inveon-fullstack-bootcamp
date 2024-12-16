using SmartShelf.Models.Repositories;

namespace SmartShelf.Models.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
    }
}