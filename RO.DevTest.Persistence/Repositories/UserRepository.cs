using RO.DevTest.Application.Contracts.Persistence.Repositories;

using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Persistence.Repositories;

public class UserRepository(DefaultContext context) : BaseRepository<User>(context), IUserRepository
{
    public Task<User> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }
}
