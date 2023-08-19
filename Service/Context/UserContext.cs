using Microsoft.EntityFrameworkCore;
using Service.Entities;

namespace Service.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
    }
}
