using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantPoll.Models;

namespace RestaurantPoll.Tests
{
    [TestClass]
    public class PollTest : Poll
    {
        [TestMethod]
        public void FindOrCreatePollCreateNewPollTest()
        {
            // Votação ainda não existe, deve ser criada.
            Poll.pollDay = new DateTime(2000, 01, 01);
            Assert.AreEqual(Poll.polls.Count, 0);
            Poll.FindOrCreatePoll();
            Assert.AreEqual(Poll.polls.Count, 1);
        }

        [TestMethod]
        public void FindOrCreatePollCorrectStartAndEndDatesTest()
        {
            Poll.pollDay = new DateTime(2015, 04, 08);
            DateTime monday = new DateTime(2015, 04, 06);
            DateTime friday = new DateTime(2015, 04, 10);
            Poll poll = Poll.FindOrCreatePoll();
            Assert.AreEqual(poll.Start, monday);
            Assert.AreEqual(poll.End, friday);
        }

        [TestMethod]
        public void GetPollDayTest()
        {
            DateTime pollDay = new DateTime(2015, 04, 08);
            Poll.pollDay = pollDay;
            Assert.AreEqual(Poll.GetPollDay(), pollDay);
        }
    }
}
