using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task AddAsync(User user);
    }
}