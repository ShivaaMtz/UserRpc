using System.Data.Entity;
using Service.Context;
using Service.Entities;

namespace Service.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _userContext;

        public UserRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<User> AddAsync(User newUser)
        {
            var user = await _userContext.User.AddAsync(newUser);

            var a = await _userContext.SaveChangesAsync();

            return user.Entity;
        }

        public async Task<User> GetAsync(Guid id)
        {
            var result = await _userContext.User.FindAsync(id);
                
            return result;
        }
    }
}
