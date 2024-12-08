using System.Collections.Generic;
using System.Linq;

namespace InveonBootcamp.Models.Repositories
{
    public class BookRepository
    {
        private static readonly List<Book> books = new List<Book>
        {
            new Book() { Description = "Book 1", Id = 1, Title = "Book 1", Author = "Orhan Pamuk", Price = 100.00m },
            new Book() { Description = "Book 2", Id = 2, Title = "Book 2", Author = "Tolstoy", Price = 200.00m }
        };
        
        public Task<List<Book>> GetAllBooks()
        {
            return Task.FromResult(books);
        }

        public Book? GetBookById(int bookId)
        {
            var book = books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
            {
                Console.WriteLine($"Book with ID {bookId} not found.");
            }
            return book;
        }

        public void AddBook(Book book)
        {
            books.Add(book);
        }

        public void UpdateBook(int id, Book updatedBook)
        {
            var book = GetBookById(id);
            if (book != null)
            {
                book.Title = updatedBook.Title;
                book.Author = updatedBook.Author;
            }
        }

        public void DeleteBook(int id)
        {
            var book = GetBookById(id);
            if (book != null)
            {
                books.Remove(book);
            }
        }
    }
}