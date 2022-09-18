
namespace ComplexTypes.Model
{
    public class User
    {
        private User() { }

        public User(string login, string password, UserProfile profile)
        {
            Login = login;
            Profile = profile;
            Password = password;
        }

        public int Id { get; set; }

        public string? Login { get; set; }

        public string? Password { get; set; }

        private UserProfile? Profile { get; set; }

        public override string ToString()
        {
            return $"Name: {Profile?.Name}  Age: {Profile?.Age}  Login: {Login} Password: {Password}";
        }
    }
}
