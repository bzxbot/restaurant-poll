using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
            var joao = new User(1, "joao@dbserver.com", "db");
            var maria = new User(2, "maria@dbserver.com", "db");
            var jose = new User(3, "jose@dbserver.com", "db");
            Users = new List<User>();
            Users.Add(joao);
            Users.Add(maria);
            Users.Add(jose);
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