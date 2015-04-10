using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantPoll.Models
{
    public class Poll
    {
        public DateTime Start { get; private set;}
        public DateTime End { get; private set; }
        public List<Day> Days { get; private set; }

        private static DateTime pollDay = DateTime.Now.Date;
        private static List<Poll> polls = new List<Poll>();

        public Poll()
        {
            Days = new List<Day>();
        }

        public static Poll FindOrCreatePoll()
        {
            Poll poll = FindPoll(pollDay);

            // Not found, let's create it.
            if (poll == null)
            {
                poll = CreatePoll(pollDay);
                polls.Add(poll);
            }

            return poll;
        }

        public static DateTime GetPollDay()
        {
            return pollDay;
        }

        public static void NextPollDay()
        {
            if (pollDay.DayOfWeek == DayOfWeek.Friday)
                pollDay = pollDay.AddDays(3);
            else
                pollDay = pollDay.AddDays(1);
        }

        public Result GetMondayResult()
        {
            return GetDayResult(Start);
        }

        public Result GetTuesdayResult()
        {
            return GetDayResult(Start.AddDays(1));
        }

        public Result GetWednesdayResult()
        {
            return GetDayResult(Start.AddDays(2));
        }

        public Result GetThursdayResult()
        {
            return GetDayResult(Start.AddDays(3));
        }

        public Result GetFridayResult()
        {
            return GetDayResult(Start.AddDays(4));
        }

        public Result GetDayResult(DateTime date)
        {
            Day day = Days.Find(d => d.Date == date);
            Restaurant restaurant = day.GetHighestVoted();
            int votes = day.GetHighestVotedCount();
            Result result = new Result
            {
                Name = restaurant != null ? restaurant.Name : "Nenhum resultado",
                Votes = votes
            };
            return result;
        }

        public Day GetCurrentDay()
        {
            Day day = null;

            foreach (Day d in Days)
            {
                if (d.Date == pollDay)
                {
                    day = d;
                }
            }

            SetRestaurantsForDay(day);

            return day;
        }

        protected static Poll CreatePoll(DateTime dayOfWeek)
        {
            Poll week = new Poll();
            SetWeekDates(week, dayOfWeek);
            Day monday = new Day(week.Start, null);
            Day tuesday = new Day(week.Start.AddDays(1), monday);
            Day wednesday = new Day(week.Start.AddDays(2), tuesday);
            Day thursday = new Day(week.Start.AddDays(3), wednesday);
            Day friday = new Day(week.Start.AddDays(4), thursday);
            week.Days.Add(monday);
            week.Days.Add(tuesday);
            week.Days.Add(wednesday);
            week.Days.Add(thursday);
            week.Days.Add(friday);
            return week;
        }

        public void SetRestaurantsForDay(Day day)
        {
            if(day.Restaurants.Count != 0)
                return;

            if (day.Date.DayOfWeek == DayOfWeek.Monday)
            {
                day.Restaurants = Restaurant.Restaurants;
            }
            else
            {
                // Remove top voted.
                var topVoted = day.Previous.GetHighestVoted();
                if (topVoted == null && day.Previous.Restaurants.Count == 0)
                {
                    day.Restaurants = Restaurant.Restaurants;
                }
                else if (topVoted == null)
                {
                    day.Restaurants = day.Previous.Restaurants;
                }
                else
                {
                    var previousRestaurants = day.Previous.Restaurants;
                    List<Restaurant> copyOfPrevious = new List<Restaurant>(previousRestaurants);
                    copyOfPrevious.Remove(topVoted);
                    day.Restaurants = copyOfPrevious;
                }
            }
        }

        public static bool IsValidDay(DateTime dateTime)
        {
            return !(dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday);
        }

        public static Poll FindPoll(DateTime date) 
        {
            foreach (Poll week in polls)
            {
                if (date.Date >= week.Start && date.Date <= week.End)
                {
                    return week;
                }
            }

            return null;
        }

        private static Poll SetWeekDates(Poll week, DateTime dateTime)
        {
            int delta = DayOfWeek.Monday - dateTime.DayOfWeek;
            week.Start = dateTime.Date.AddDays(delta);            
            delta = DayOfWeek.Friday - dateTime.DayOfWeek;
            week.End = dateTime.Date.AddDays(delta);
            return week;
        }
    }
}
