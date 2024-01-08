using Microsoft.AspNetCore.Mvc;

namespace TestingWithDb.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController
{
    private readonly IFavoriteService _favoriteService;

    public ProductController(IFavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }

    [HttpPost]
    public async Task FavoriteBook([FromBody]FavoriteBookRequest request)
    {
        await _favoriteService.FavoriteProduct(request.ProductId, request.UserId);
    }
}

public record FavoriteBookRequest(int ProductId, int UserId);