using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RestaurantPoll.Models
{
    public class User
    {
        private static List<User> Users;

        public static List<User> GetAllUsers()
        {
            return Users;
        }

        static User()
        {
            // Criação da lista de usuários padrão do sistema.
            var joao = new User(1, "joao@example.com", GetHash("db"));
            var maria = new User(2, "maria@example.com", GetHash("db"));
            var jose = new User(3, "jose@example.com", GetHash("db"));
            Users = new List<User>();
            Users.Add(joao);
            Users.Add(maria);
            Users.Add(jose);
        }

        public static RestaurantPoll.Models.User Authenticate(string email, string password)
        {
            RestaurantPoll.Models.User user =
                RestaurantPoll.Models.User.GetAllUsers().Find(u => u.Email == email && u.Password == GetHash(password));

            return user;
        }

        public static string GetHash(string password)
        {
            HashAlgorithm algorithm = SHA1.Create();
            return System.Convert.ToBase64String(algorithm.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        public int Id { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        public User(int id, string email, string password)
        {
            this.Id = id;
            this.Email = email;
            this.Password = password;
        }
    }
}