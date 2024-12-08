namespace InveonBootcamp.Models.Repositories;
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
    }
