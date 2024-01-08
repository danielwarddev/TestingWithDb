namespace TestingWithDb.Database;

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public virtual ICollection<ProductFavorite> ProductFavorite { get; set; } = new List<ProductFavorite>();
    public virtual ICollection<ProductReview> ProductReview { get; set; } = new List<ProductReview>();
}
