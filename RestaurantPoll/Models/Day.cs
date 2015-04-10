using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantPoll.Models
{
    public class Day
    {
        public List<Restaurant> Restaurants = new List<Restaurant>();
        public DateTime Date;
        public Day Previous { get; private set; }
        
        protected Dictionary<User, Restaurant> Votes;

        public Day(DateTime date, Day previous)
        {
            this.Date = date;
            this.Previous = previous;
            Votes = new Dictionary<User, Restaurant>();
        }

        public void Vote(User user, Restaurant restaurant)
        {
            if (!HasUserVoted(user))
                Votes.Add(user, restaurant);
        }

        public bool HasUserVoted(User user)
        {
            return Votes.Keys.Where(u => u.Id == user.Id).ToList().Count != 0;
        }

        public Restaurant GetHighestVoted()
        {
            if (Votes.Keys.Count == 0)
                return null;

            var max = 0;
            Restaurant removed = null;
            foreach(Restaurant r in Restaurants)
            {
                var count = Votes.Where(res => res.Value.Name == r.Name).ToList().Count;
                if (count > max)
                {
                    max = count;
                    removed = r;
                }
            }

            return removed;
        }

        public int GetHighestVotedCount()
        {
            Restaurant restaurant = GetHighestVoted();

            if (restaurant == null)
            {
                return 0;
            }

            return Votes.Where(res => res.Value.Name == restaurant.Name).ToList().Count;
        }

        public void SetRestaurants()
        {
            if (Restaurants.Count != 0)
                return;

            if (Date.DayOfWeek == DayOfWeek.Monday)
            {
                Restaurants = Restaurant.Restaurants;
                return;
            }
            
            // Quatro casos são tratados aqui:
            // Primeiro, se o dia anterior não foi definido.
            // Idealmente, isso só acontece na segunda-feira.
            // Segundo, o caso do dia anterior não ter havido votação.
            // Terceiro, a votação estava disponível, mas não houve voto.
            // Quarto, quando houve voto e um restaurante é removido da lista.
            if (Previous == null)
            {
                Restaurants = Restaurant.Restaurants;
                return;
            }

            var topVoted = Previous.GetHighestVoted();

            if (topVoted == null && Previous.Restaurants.Count == 0)
            {
                Restaurants = Restaurant.Restaurants;
            }
            else if (topVoted == null)
            {
                Restaurants = Previous.Restaurants;
            }
            else
            {
                // Retira o restaurante mais votado do dia anterior.
                var previousRestaurants = Previous.Restaurants;
                List<Restaurant> copyOfPrevious = new List<Restaurant>(previousRestaurants);
                copyOfPrevious.Remove(topVoted);
                Restaurants = copyOfPrevious;
            }
            
        }
    }
}