using System.Collections.Generic;

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
            var ru = new Restaurant(3, "Restaurante Universitário");
            var bar15 = new Restaurant(4, "Bar 15");
            var barDaFamecos = new Restaurant(5, "Bar da Famecos");
            Restaurants = new List<Restaurant>();
            Restaurants.Add(panorama);
            Restaurants.Add(intervalo);
            Restaurants.Add(ru);
            Restaurants.Add(bar15);
            Restaurants.Add(barDaFamecos);
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