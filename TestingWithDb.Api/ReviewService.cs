using Microsoft.EntityFrameworkCore;
using TestingWithDb.Database;

namespace TestingWithDb.Api;

public class ReviewService
{
    private readonly ProductContext _context;

    public ReviewService(ProductContext context)
    {
        _context = context;
    }

    public async Task ReviewProduct(int productId, int userId, string reviewContent)
    {
        var test = await _context.ProductReview.FirstOrDefaultAsync(x =>
            x.ProductId == productId &&
            x.UserId == userId);
        var existingReview = await _context.ProductReview.FirstOrDefaultAsync(x =>
            x.ProductId == productId &&
            x.UserId == userId);

        if (existingReview != null)
        {
            existingReview.ReviewContent = reviewContent;
        }
        else
        {
            await _context.ProductReview.AddAsync(new ProductReview
            {
                ProductId = productId,
                UserId = userId,
                ReviewContent = reviewContent
            });
        }

        await _context.SaveChangesAsync();
    }
}
