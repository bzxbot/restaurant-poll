using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantPoll.Models;
using System.Collections.Generic;

namespace RestaurantPoll.Tests.Models
{
    [TestClass]
    public class DayTest
    {
        [TestMethod]
        public void VoteTest()
        {
            var day = new Day(new DateTime(2015, 12, 12), null);
            var joao = new User(1, "joao@dbserver.com", "db");
            // Todos os restaurantes.
            day.SetRestaurants();
            var panorama = new Restaurant(1, "Panorama");
            day.Vote(joao, panorama);
            Assert.IsTrue(day.HasUserVoted(joao));
        }

        [TestMethod]
        public void GetHighestVotedTest()
        {
            // Estória 1 e 3.
            // Restaurante mais votado.
            var day = new Day(new DateTime(2015, 12, 12), null);
            var joao = new User(1, "joao@dbserver.com", "db");
            var maria = new User(2, "maria@dbserver.com", "db");
            var jose = new User(3, "jose@dbserver.com", "db");
            // Todos os restaurantes.
            day.SetRestaurants();
            var panorama = new Restaurant(1, "Panorama");
            var intervalo = new Restaurant(2, "Intervalo 50");
            day.Vote(joao, panorama);
            day.Vote(maria, panorama);
            day.Vote(jose, intervalo);
            // João e Maria votam novamente, o vencedor continua sendo Panorama.
            day.Vote(joao, intervalo);
            day.Vote(maria, intervalo);
            Assert.AreEqual(day.GetHighestVoted().Id, panorama.Id);
        }

        [TestMethod]
        public void GetHighestVotedCountTest()
        {
            // Restaurante com maior número de votos.
            var day = new Day(new DateTime(2015, 12, 12), null);
            var joao = new User(1, "joao@dbserver.com", "db");
            var maria = new User(2, "maria@dbserver.com", "db");
            var jose = new User(3, "jose@dbserver.com", "db");
            // Todos os restaurantes.
            day.SetRestaurants();
            var panorama = new Restaurant(1, "Panorama");
            var intervalo = new Restaurant(2, "Intervalo 50");
            day.Vote(joao, panorama);
            day.Vote(maria, panorama);
            day.Vote(jose, intervalo);
            Assert.AreEqual(day.GetHighestVotedCount(), 2);
        }

        [TestMethod]
        public void SetRestaurantsTest()
        {
            // Estória 2.
            // Um restaurante escolhido em um dia anterior não pode ser escolhido novamente.
            var day = new Day(new DateTime(2015, 12, 12), null);
            var joao = new User(1, "joao@dbserver.com", "db");
            var maria = new User(2, "maria@dbserver.com", "db");
            var jose = new User(3, "jose@dbserver.com", "db");
            // Todos os restaurantes.
            day.SetRestaurants();
            var panorama = new Restaurant(1, "Panorama");
            var intervalo = new Restaurant(2, "Intervalo 50");
            Assert.AreEqual(day.Restaurants.Count, 5);
            day.Vote(joao, panorama);
            day.Vote(maria, panorama);
            day.Vote(jose, intervalo);
            var nextDay = new Day(new DateTime(2015, 12, 13), day);
            nextDay.SetRestaurants();
            Assert.AreEqual(nextDay.Restaurants.Count, 4);
        }
    }
}
