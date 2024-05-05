using AutoFixture;
using FluentAssertions;
using TestingWithDb.Api;
using TestingWithDb.Domain.AggregatesModel;
using TestingWithDb.IntegrationTests.Setup;

namespace TestingWithDb.IntegrationTests;

public class FavoriteServiceTests : DatabaseTest, IAsyncLifetime
{
    private readonly FavoriteService _service;
    private Product _existingProduct = null!;
    private User _existingUser = null!;

    public FavoriteServiceTests(IntegrationTestFactory factory) : base(factory)
    {
        _service = new FavoriteService(DbContext);
    }

    public new async Task InitializeAsync()
    {
        await SeedDb();
    }

    [Fact]
    public async Task When_User_Has_Not_Favorited_Product_Yet_Then_Inserts_Favorite_Record()
    {
        var expectedFavorite = new ProductFavorite
        {
            ProductId = _existingProduct.Id,
            UserId = _existingUser.Id
        };
        await _service.FavoriteProduct(_existingProduct.Id, _existingUser.Id);

        var allFavorites = DbContext.ProductFavorites.ToList();
        allFavorites
            .Should().ContainSingle()
            .Which.Should().BeEquivalentTo(expectedFavorite, options => options
                .Excluding(x => x.Id)
                .Excluding(x => x.Product)
                .Excluding(x => x.User)
            );
    }

    [Fact]
    public async Task When_User_Has_Already_Favorited_Product_Then_Does_Not_Insert_Another_Favorite_Record()
    {
        await Insert(new ProductFavorite
        {
            ProductId = _existingProduct.Id,
            UserId = _existingUser.Id
        });

        await _service.FavoriteProduct(_existingProduct.Id, _existingUser.Id);

        var allFavorites = DbContext.ProductFavorites.ToList();
        allFavorites.Should().ContainSingle();
    }

    private async Task SeedDb()
    {
        _existingProduct = Fixture.Create<Product>();
        _existingUser = Fixture.Create<User>();

        await Insert(_existingUser);
        await Insert(_existingProduct);
    }
}