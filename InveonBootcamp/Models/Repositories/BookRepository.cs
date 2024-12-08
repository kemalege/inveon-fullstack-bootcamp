using System.Collections.Generic;
using System.Linq;

namespace InveonBootcamp.Models.Repositories
{
    public class BookRepository
    {
        private static readonly List<Book> books = new List<Book>
        {
            new Book() { Description = "A gripping historical novel", Id = 1, Title = "The Midnight Library", Author = "Matt Haig", Price = 150.00m },
            new Book() { Description = "A classic of Russian literature", Id = 2, Title = "War and Peace", Author = "Leo Tolstoy", Price = 200.00m },
            new Book() { Description = "An inspiring self-help book", Id = 3, Title = "Atomic Habits", Author = "James Clear", Price = 120.00m },
            new Book() { Description = "A thrilling fantasy adventure", Id = 4, Title = "The Name of the Wind", Author = "Patrick Rothfuss", Price = 180.00m },
            new Book() { Description = "A powerful memoir of resilience", Id = 5, Title = "Becoming", Author = "Michelle Obama", Price = 130.00m },
            new Book() { Description = "A tale of love and tragedy", Id = 6, Title = "Pride and Prejudice", Author = "Jane Austen", Price = 140.00m },
            new Book() { Description = "A journey through the cosmos", Id = 7, Title = "Cosmos", Author = "Carl Sagan", Price = 170.00m },
            new Book() { Description = "A compelling science fiction novel", Id = 8, Title = "Dune", Author = "Frank Herbert", Price = 160.00m },
        };
        
        public Task<List<Book>> GetAllBooksAsync(int skip, int take)
        {
            return Task.FromResult(books.Skip(skip).Take(take).ToList());
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