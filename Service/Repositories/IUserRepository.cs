using Service.Dtos;
using Service.Entities;

namespace Service.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User newUser);

        Task<User> GetAsync(Guid id);
    }
}
