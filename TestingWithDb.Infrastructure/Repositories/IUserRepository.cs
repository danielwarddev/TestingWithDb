using TestingWithDb.Domain.AggregatesModel;

namespace TestingWithDb.Infrastructure.Repositories;

public interface IUserRepository
{
    public List<User> GetUsers();
}