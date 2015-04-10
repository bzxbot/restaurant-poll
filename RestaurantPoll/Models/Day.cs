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
        private Dictionary<User, Restaurant> Votes;
        public Day Previous { get; private set; }

        public Day(DateTime date, Day previous)
        {
            this.Date = date;
            this.Previous = previous;
            Votes = new Dictionary<User, Restaurant>();
        }

        public bool HasUserVoted(User user)
        {
            return Votes.Keys.Where(u => u.Id == user.Id).ToList().Count != 0;
        }

        public void Vote(User user, Restaurant restaurant)
        {
            Votes.Add(user, restaurant);
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
    }
}