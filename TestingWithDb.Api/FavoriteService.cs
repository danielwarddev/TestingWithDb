using Microsoft.EntityFrameworkCore;
using TestingWithDb.Domain.AggregatesModel;
using TestingWithDb.Infrastructure;

namespace TestingWithDb.Api;

public interface IFavoriteService
{
    Task FavoriteProduct(int productId, int userId);
}

public class FavoriteService : IFavoriteService
{
    private readonly ProductDbContext _context;

    public FavoriteService(ProductDbContext context)
    {
        _context = context;
    }

    public async Task FavoriteProduct(int productId, int userId)
    {
        var existingFavorite = await _context.ProductFavorites.FirstOrDefaultAsync(x =>
            x.ProductId == productId &&
            x.UserId == userId
        );

        if (existingFavorite != null) return;

        await _context.ProductFavorites.AddAsync(new ProductFavorite
        {
            ProductId = productId,
            UserId = userId
        });
        await _context.SaveChangesAsync();
    }
}