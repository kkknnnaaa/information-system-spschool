using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using spschool;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddSportsman()
        {
            ListSportsmanJson sportsmanList = new ListSportsmanJson();
            Sportsman sportsman = new Sportsman();
            sportsman.Code = 0;
            sportsman.FIO = "qq qq qq";
            sportsman.Sport = "Sport";
            sportsman.Team = 0;
            sportsman.BrDate = new DateTime();
            sportsman.Title = "qq";
            sportsmanList.bd.Add(sportsman);

            Assert.AreEqual(sportsmanList.bd.Count, 1);
        }

        [TestMethod]
        public void RemoveSportsman()
        {
            ListSportsmanJson sportsmanList = new ListSportsmanJson();
            Sportsman sportsman = new Sportsman();
            sportsman.Code = 0;
            sportsman.FIO = "qq qq qq";
            sportsman.Sport = "Sport";
            sportsman.Team = 0;
            sportsman.BrDate = new DateTime();
            sportsman.Title = "qq";
            sportsmanList.bd.Remove(sportsman);

            Assert.AreEqual(sportsmanList.bd.Count, 0);
        }

        [TestMethod]
        public void ContainsSportsman()
        {
            ListSportsmanJson sportsmanList = new ListSportsmanJson();
            Sportsman sportsman = new Sportsman();
            sportsman.Code = 0;
            sportsman.FIO = "qq qq qq";
            sportsman.Sport = "Sport";
            sportsman.Team = 0;
            sportsman.BrDate = new DateTime();
            sportsman.Title = "qq";
            sportsmanList.bd.Add(sportsman);
            bool t = sportsmanList.bd.Contains(sportsman);

            Assert.AreEqual(t, true); 
        }
    }
}
