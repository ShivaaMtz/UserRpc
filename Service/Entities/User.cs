using System.ComponentModel.DataAnnotations;

namespace Service.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string PasswordHash { get; set; }
    }
}
