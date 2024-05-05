namespace TestingWithDb.Domain.AggregatesModel;

public class ProductReview
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public required string ReviewContent { get; set; }

    public virtual Product Product { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}