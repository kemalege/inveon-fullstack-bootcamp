using InveonBootcamp.Models.Repositories;

namespace InveonBootcamp.Models.Mappers;

public static class BookMapper
{
    public static BookDto ToDto(this Book book)
    {
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
