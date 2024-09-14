using Olympia.Domain;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required string Category { get; set; }
    public required string SubCategory { get; set; }

    public ICollection<Review> Reviews { get; set; }
}