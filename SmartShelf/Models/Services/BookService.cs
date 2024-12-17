using Microsoft.EntityFrameworkCore;
using SmartShelf.Models.Repositories;

namespace SmartShelf.Models.Services;

public class BookService(AppDbContext context) : IBookService
{
    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await context.Books.ToListAsync();
    }

    public async Task<Book> GetBookByIdAsync(int id)
    {
        return await context.Books.FirstOrDefaultAsync(b => b.Id == id);
    }
}