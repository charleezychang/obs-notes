namespace Olympia.Domain
{
    public class Review
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Title { get; set; }
        public required string Content { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
