namespace Client.ViewModels
{
    public class UserVM
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string PasswordHash { get; set; }
    }
}
