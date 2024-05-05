using AutoFixture;
using FluentAssertions;
using TestingWithDb.Api;
using TestingWithDb.Domain.AggregatesModel;
using TestingWithDb.IntegrationTests.Setup;

namespace TestingWithDb.IntegrationTests;

public class ReviewServiceTests : DatabaseTest, IAsyncLifetime
{
    private readonly ReviewService _service;
    private Product _existingProduct = null!;
    private User _existingUser = null!;

    public ReviewServiceTests(IntegrationTestFactory factory) : base(factory)
    {
        _service = new ReviewService(DbContext);
    }

    public new async Task InitializeAsync()
    {
        await SeedDb();
    }

    [Fact]
    public async Task When_User_Has_Not_Reviewed_Product_Yet_Then_Inserts_Review_Record()
    {
        var expectedReview = new ProductReview
        {
            ProductId = _existingProduct.Id,
            UserId = _existingUser.Id,
            ReviewContent = Fixture.Create<string>()
        };
        await _service.ReviewProduct(_existingProduct.Id, _existingUser.Id, expectedReview.ReviewContent);

        var allReviews = DbContext.ProductReviews.ToList();
        allReviews
            .Should().ContainSingle()
            .Which.Should().BeEquivalentTo(expectedReview, options => options
                .Excluding(x => x.Id)
                .Excluding(x => x.Product)
                .Excluding(x => x.User)
            );
    }

    [Fact]
    public async Task When_User_Has_Already_Reviewed_Product_Then_Updates_Review_Content()
    {
        await Insert(new ProductReview
        {
            ProductId = _existingProduct.Id,
            UserId = _existingUser.Id,
            ReviewContent = "old review content"
        });

        await _service.ReviewProduct(_existingProduct.Id, _existingUser.Id, "new review content");

        var allFavorites = DbContext.ProductReviews.ToList();
        allFavorites
            .Should().ContainSingle()
            .Which.ReviewContent.Should().BeEquivalentTo("new review content");
    }

    private async Task SeedDb()
    {
        _existingProduct = Fixture.Create<Product>();
        _existingUser = Fixture.Create<User>();

        await Insert(_existingUser);
        await Insert(_existingProduct);
    }
}