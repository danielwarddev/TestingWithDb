using AutoFixture;
using TestingWithDb.Infrastructure;

namespace TestingWithDb.IntegrationTests.Setup;

[Collection(nameof(DatabaseTestCollection))]
public abstract class DatabaseTest : IAsyncLifetime
{
    protected readonly ProductDbContext DbContext;
    protected readonly Fixture Fixture;
    private readonly Func<Task> _resetDatabase;

    public DatabaseTest(IntegrationTestFactory factory)
    {
        _resetDatabase = factory.ResetDatabase;
        DbContext = factory.DbContext;
        Fixture = new Fixture();
        Fixture.Customize(new NoCircularReferencesCustomization());
        Fixture.Customize(new IgnoreVirtualMembersCustomization());
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        return _resetDatabase();
    }

    public async Task Insert<T>(T entity) where T : class
    {
        await DbContext.AddAsync(entity);
        await DbContext.SaveChangesAsync();
    }
}