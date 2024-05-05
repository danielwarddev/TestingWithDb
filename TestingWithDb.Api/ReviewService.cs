using Microsoft.EntityFrameworkCore;
using TestingWithDb.Domain.AggregatesModel;
using TestingWithDb.Infrastructure;

namespace TestingWithDb.Api;

public class ReviewService
{
    private readonly ProductDbContext _context;

    public ReviewService(ProductDbContext context)
    {
        _context = context;
    }

    public async Task ReviewProduct(int productId, int userId, string reviewContent)
    {
        var test = await _context.ProductReviews.FirstOrDefaultAsync(x =>
            x.ProductId == productId &&
            x.UserId == userId);
        var existingReview = await _context.ProductReviews.FirstOrDefaultAsync(x =>
            x.ProductId == productId &&
            x.UserId == userId);

        if (existingReview != null)
            existingReview.ReviewContent = reviewContent;
        else
            await _context.ProductReviews.AddAsync(new ProductReview
            {
                ProductId = productId,
                UserId = userId,
                ReviewContent = reviewContent
            });

        await _context.SaveChangesAsync();
    }
}