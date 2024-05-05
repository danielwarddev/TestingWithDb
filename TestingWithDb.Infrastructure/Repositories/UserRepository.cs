using TestingWithDb.Domain.AggregatesModel;

namespace TestingWithDb.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ProductDbContext _dbContext;

    public UserRepository(ProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<User> GetUsers()
    {
        return _dbContext.Users.ToList();
    }
}