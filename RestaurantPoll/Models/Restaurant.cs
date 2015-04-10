using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantPoll.Models
{
    public class Restaurant
    {
        public static List<Restaurant> Restaurants;        

        static Restaurant()
        {
            // Criação da lista de restaurantes padrão do sistema.
            var panorama = new Restaurant(1, "Panorama");
            var intervalo = new Restaurant(2, "Intervalo 50");
            Restaurants = new List<Restaurant>();
            Restaurants.Add(panorama);
            Restaurants.Add(intervalo);
        }

        public int Id { get; private set; }
        public string Name { get; private set; }

        public Restaurant(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}