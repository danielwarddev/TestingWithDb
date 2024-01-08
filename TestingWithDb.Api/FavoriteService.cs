using Microsoft.EntityFrameworkCore;
using TestingWithDb.Database;

namespace TestingWithDb.Api;

public interface IFavoriteService
{
    Task FavoriteProduct(int productId, int userId);
}

public class FavoriteService : IFavoriteService
{
    private readonly ProductContext _context;

    public FavoriteService(ProductContext context)
    {
        _context = context;
    }

    public async Task FavoriteProduct(int productId, int userId)
    {
        var existingFavorite = await _context.ProductFavorite.FirstOrDefaultAsync(x =>
            x.ProductId == productId &&
            x.UserId == userId
        );

        if (existingFavorite != null)
        {
            return;
        }

        await _context.ProductFavorite.AddAsync(new ProductFavorite
        {
            ProductId = productId,
            UserId = userId
        });
        await _context.SaveChangesAsync();
    }
}
